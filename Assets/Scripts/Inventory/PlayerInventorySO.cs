using System;
using System.Collections.Generic;
using KittyFarm.Data;
using UnityEngine;

namespace KittyFarm.InventorySystem
{
    [CreateAssetMenu(fileName = "PlayerInventory", menuName = "Inventory/PlayerInventory")]
    public class PlayerInventorySO : ScriptableObject
    {
        public const int MaxSize = 9;
        
        [SerializeField] private List<InventoryItem> items;
        public IEnumerable<InventoryItem> AllItems => items;

        public event Action<int, InventoryItem> ItemChanged;

        public bool AddItem(Item item)
        {
            var index = FindIndexToAddItem(item.ItemData);
            if (index == -1) return false;

            var inventoryItem = items[index];
            inventoryItem.itemId = item.ItemData.Id;
            inventoryItem.count += item.Count;

            ItemChanged?.Invoke(index, inventoryItem);

            return true;
        }

        public bool AddItem(ItemDataSO itemData, int itemAmount)
        {
            var index = FindIndexToAddItem(itemData);
            if (index == -1) return false;

            var inventoryItem = items[index];
            inventoryItem.itemId = itemData.Id;
            inventoryItem.count += itemAmount;

            ItemChanged?.Invoke(index, inventoryItem);

            return true;
        }

        public void RemoveItemAll(int index)
        {
            var inventoryItem = items[index];

            inventoryItem.itemId = -1;
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
                if (item.itemId <= 0) continue;

                // 找到已存在的物品，直接返回它
                if (item.itemId == itemData.Id) return index;
                // 记录最小的空位索引
                if (emptyIndex != -1) continue;
                if (item.itemId <= 0 || item.count == 0) emptyIndex = index;

                index++;
            }

            return emptyIndex;
        }

        public void LoadData(PlayerInventoryData inventoryData)
        {
            items = inventoryData.ItemList;
            
            if (items.Count != 0) return;
            for(var index = 0; index < MaxSize; index++) items.Add(new InventoryItem());
        }
    }
}