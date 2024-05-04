using KittyFarm.Data;
using KittyFarm.InventorySystem;
using UnityEngine;

namespace KittyFarm.UI
{
    public class ItemSlotGroup : MonoBehaviour
    {
        public ItemSlot SelectedSlot { get; private set; }
        
        private PlayerInventory Inventory => GameDataCenter.Instance.PlayerInventory;
        private ItemSlot[] slots;

        private void Awake()
        {
            slots = GetComponentsInChildren<ItemSlot>();
        }

        private void OnEnable()
        {
            PlayerInventory.ItemChanged += SetSlotDataAt;
        }

        private void OnDisable()
        {
            PlayerInventory.ItemChanged -= SetSlotDataAt;
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
            foreach (var item in Inventory.Items)
            {
                SetSlotDataAt(index++, item);
            }
        }

        private void SetSlotDataAt(int slotIndex, InventoryItem item)
        {
            var targetSlot = slots[slotIndex];
            targetSlot.Item = item;
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