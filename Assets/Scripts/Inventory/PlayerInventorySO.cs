using System;
using System.Collections.Generic;
using UnityEngine;

namespace KittyFarm.InventorySystem
{
    [CreateAssetMenu(fileName = "PlayerInventory", menuName = "Inventory/PlayerInventory")]
    public class PlayerInventorySO : ScriptableObject
    {
        [field: SerializeField] private List<InventoryItem> items { get; set; } = new(MaxCapacity);
        public IEnumerable<InventoryItem> Items => items;

        public event Action<int, InventoryItem> ItemChanged;

        private const int MaxCapacity = 10;

        public bool AddItem(Item item)
        {
            var index = FindIndexToAddItem(item.ItemData);
            if (index == -1) return false;

            var inventoryItem = items[index];
            inventoryItem.itemData = item.ItemData;
            inventoryItem.count += item.Count;

            ItemChanged?.Invoke(index, inventoryItem);

            return true;
        }

        public bool AddItem(ItemDataSO itemData, int itemAmount)
        {
            var index = FindIndexToAddItem(itemData);
            if (index == -1) return false;

            var inventoryItem = items[index];
            inventoryItem.itemData = itemData;
            inventoryItem.count += itemAmount;

            ItemChanged?.Invoke(index, inventoryItem);

            return true;
        }

        public void RemoveItemAll(int index)
        {
            var inventoryItem = items[index];

            inventoryItem.itemData = null;
            inventoryItem.count = 0;

            ItemChanged?.Invoke(index, inventoryItem);
        }

        public void SwapTwoItems(int index1, int index2)
        {
            var item1 = items[index1];
            var item2 = items[index2];

            items[index1] = item2;
            items[index2] = item1;

            ItemChanged?.Invoke(index1, item2);
            ItemChanged?.Invoke(index2, item1);
        }

        private int FindIndexToAddItem(ItemDataSO itemData)
        {
            var emptyIndex = -1;
            var index = 0;
            foreach (var item in items)
            {
                // 找到已存在的物品，直接返回它
                if (item.itemData == itemData) return index;
                // 记录最小的空位索引
                if (emptyIndex != -1) continue;
                if (item.itemData == null || item.count == 0) emptyIndex = index;

                index++;
            }

            return emptyIndex;
        }
    }
}