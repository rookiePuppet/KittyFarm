using KittyFarm.Data;
using KittyFarm.Service;
using UnityEngine;
using UnityEngine.EventSystems;

namespace KittyFarm.UI
{
    public class ItemSlotDraggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private ItemSlot slot;
        private PlayerInventory Inventory => GameDataCenter.Instance.PlayerInventory;

        private DragItemWidget dragItem;

        private void Awake()
        {
            slot = GetComponent<ItemSlot>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (slot.IsEmpty) return;

            dragItem = UIManager.Instance.ShowUI<DragItemWidget>();

            dragItem.ItemSprite = slot.ItemSprite;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (slot.IsEmpty) return;

            dragItem.Position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (slot.IsEmpty) return;

            var draggedSlot = eventData.pointerDrag.GetComponent<ItemSlot>();

            var raycastHit = Physics2D.Raycast(eventData.position, Vector3.forward, 5f, LayerMask.GetMask("UI"));
            if (raycastHit)
            {
                if (raycastHit.transform.TryGetComponent(typeof(ShopWindow), out var shopWindow))
                {
                    ((ShopWindow)shopWindow).OnItemDraggedIn(slot.ItemData, slot.Item.count);
                }
                else if (raycastHit.transform.TryGetComponent(typeof(ItemSlot), out var targetSlot))
                {
                    if (targetSlot == draggedSlot) return;

                    Inventory.SwapTwoItems(draggedSlot.Index, ((ItemSlot)targetSlot).Index);
                }
            }
            else
            {
                var itemData = draggedSlot.ItemData;
                var amount = draggedSlot.Item.count;

                var position = ServiceCenter.Get<ICameraService>().ScreenToWorldPoint(eventData.position);
                ServiceCenter.Get<IItemService>().SpawnItemAt(position, itemData, amount);

                Inventory.RemoveItemAll(draggedSlot.Index);

                draggedSlot.IsSelected = false;
            }

            UIManager.Instance.HideUI<DragItemWidget>();
        }
    }
}