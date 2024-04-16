using KittyFarm.Data;
using KittyFarm.InventorySystem;
using KittyFarm.Service;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace KittyFarm.UI
{
    public class ItemSlotGroup : MonoBehaviour
    {
        [SerializeField] private Image dragImage;
        
        public ItemSlot SelectedSlot { get; private set; }
        
        private PlayerInventorySO Inventory => GameDataCenter.Instance.PlayerInventory;
        private ItemSlot[] slots;

        private void Awake()
        {
            slots = GetComponentsInChildren<ItemSlot>();
        }

        private void OnEnable()
        {
            Inventory.ItemChanged += SetSlotDataAt;
        }

        private void OnDisable()
        {
            Inventory.ItemChanged -= SetSlotDataAt;
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
            foreach (var item in Inventory.AllItems)
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

                Inventory.SwapTwoItems(draggedSlot.Index, targetSlot.Index);
            }
            else
            {
                var itemData = draggedSlot.ItemData;
                var amount = draggedSlot.Item.count;

                var position = ServiceCenter.Get<IPointerService>().ScreenToWorldPoint(eventData.position);
                ServiceCenter.Get<IItemService>().SpawnItemAt(position, itemData, amount);

                Inventory.RemoveItemAll(draggedSlot.Index);

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