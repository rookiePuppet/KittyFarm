using KittyFarm.InventorySystem;
using KittyFarm.Service;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace KittyFarm.UI
{
    public class ItemSlotGroup : MonoBehaviour
    {
        [SerializeField] private PlayerInventorySO inventory;
        [SerializeField] private Image dragImage;

        public ItemSlot SelectedSlot { get; private set; }

        private ItemSlot[] slots;

        private void Awake()
        {
            slots = GetComponentsInChildren<ItemSlot>();
        }

        private void OnEnable()
        {
            inventory.ItemChanged += SetSlotDataAt;
        }

        private void OnDisable()
        {
            inventory.ItemChanged -= SetSlotDataAt;
        }

        private void Start()
        {
            var index = 0;
            foreach (var slot in slots)
            {
                slot.Initialize(this, index++);
            }

            UpdateAllSlots();
        }

        private void UpdateAllSlots()
        {
            var index = 0;
            foreach (var item in inventory.Items)
            {
                SetSlotDataAt(index++, item);
            }
        }

        private void SetSlotDataAt(int slotIndex, InventoryItem item)
        {
            var targetSlot = slots[slotIndex];
            targetSlot.Item = item;
        }

        public void BeginDragItemSlot(PointerEventData eventData, Sprite itemSprite)
        {
            dragImage.gameObject.SetActive(true);
            dragImage.sprite = itemSprite;
        }

        public void EndDragItemSlot(PointerEventData eventData)
        {
            dragImage.gameObject.SetActive(false);
            
            var draggedSlot = eventData.pointerDrag.GetComponent<ItemSlot>();

            var raycastHit = Physics2D.Raycast(eventData.position, Vector3.forward, 5f, LayerMask.GetMask("UI"));
            if (raycastHit)
            {
                var targetSlot = raycastHit.transform.GetComponent<ItemSlot>();
                if (targetSlot == draggedSlot) return;
                
                inventory.SwapTwoItems(draggedSlot.Index, targetSlot.Index);
            }
            else
            {
                var itemData = draggedSlot.Item.itemData;
                var amount = draggedSlot.Item.count;
                
                var position =  Camera.main.ScreenToWorldPoint(eventData.position);
                ServiceCenter.Get<IItemService>().SpawnItemAt(position, itemData, amount);
                
                inventory.RemoveItemAll(draggedSlot.Index);

                draggedSlot.IsSelected = false;
            }
        }

        public void DragItemSlot(PointerEventData eventData)
        {
            dragImage.transform.position = eventData.position;
        }

        public void UpdateItemSlotSelected(ItemSlot clickedSlot)
        {
            if (clickedSlot.IsEmpty) return;

            foreach (var slot in slots)
            {
                if (slot == clickedSlot)
                {
                    slot.IsSelected = !slot.IsSelected;

                    SelectedSlot = slot.IsSelected ? slot : null;
                }
                else
                {
                    slot.IsSelected = false;
                }
            }
        }
    }
}