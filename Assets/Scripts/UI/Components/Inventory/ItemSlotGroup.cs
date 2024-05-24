using System;
using KittyFarm.Data;
using KittyFarm.InventorySystem;
using UnityEngine;

namespace KittyFarm.UI
{
    public class ItemSlotGroup : MonoBehaviour
    {
        public event Action<ItemDataSO> SelectedItemChanged;
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
            ItemSlot.OnClicked += UpdateItemSlotSelected;
        }

        private void OnDisable()
        {
            PlayerInventory.ItemChanged -= SetSlotDataAt;
            ItemSlot.OnClicked -= UpdateItemSlotSelected;
        }

        private void Start()
        {
            var index = 0;
            foreach (var slot in slots)
            {
                slot.Index = index++;
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

            if (targetSlot.IsEmpty && SelectedSlot == targetSlot)
            {
                SelectedSlot = null;
                targetSlot.IsSelected = false;
            }
        }

        private void UpdateItemSlotSelected(ItemSlot clickedSlot)
        {
            if (clickedSlot.IsEmpty) return;

            foreach (var slot in slots)
            {
                if (slot == clickedSlot)
                {
                    slot.IsSelected = !slot.IsSelected;
                    SelectedSlot = slot.IsSelected ? slot : null;
                    SelectedItemChanged?.Invoke(SelectedSlot != null ? SelectedSlot.ItemData : null);
                }
                else
                {
                    slot.IsSelected = false;
                }
            }
        }
    }
}