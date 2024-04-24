using System;
using System.Collections.Generic;
using UnityEngine;

namespace KittyFarm.InventorySystem
{
    [CreateAssetMenu(fileName = "PlayerInventory", menuName = "Inventory/PlayerInventory")]
    public class PlayerInventorySO : ScriptableObject
    {
        [SerializeField] private List<InventoryItem> items = new();

        public List<InventoryItem> Items => items;
        public event Action<int, InventoryItem> ItemChanged;

        public const string PersistentDataName = "PlayerInventory";
        public const int MaxSize = 9;

        public bool AddItem(ItemDataSO itemData, int itemAmount)
        {
            var index = FindIndexToAddItem(itemData);
            if (index == -1) return false;

            var inventoryItem = items[index];
            inventoryItem.itemId = itemData.Id;
            inventoryItem.count += itemAmount;

            ItemChanged?.Invoke(index, inventoryItem);

            //SaveData();

            return true;
        }

        public bool AddItem(Item item) => AddItem(item.ItemData, item.Count);

        public void RemoveItemAll(int index)
        {
            var inventoryItem = items[index];

            inventoryItem.itemId = -1;
            inventoryItem.count = 0;

            ItemChanged?.Invoke(index, inventoryItem);

            //SaveData();
        }

        public void SwapTwoItems(int index1, int index2)
        {
            var item1 = items[index1];
            var item2 = items[index2];

            items[index1] = item2;
            items[index2] = item1;

            ItemChanged?.Invoke(index1, item2);
            ItemChanged?.Invoke(index2, item1);

            //SaveData();
        }

        private int FindIndexToAddItem(ItemDataSO itemData)
        {
            var emptyIndex = -1;
            var index = 0;
            foreach (var item in items)
            {
                // 找到已存在的物品，直接返回它
                if (item.itemId == itemData.Id)
                {
                    return index;
                }

                // 记录最小的空位索引
                if ((item.itemId <= 0 || item.count == 0) && emptyIndex == -1)
                {
                    emptyIndex = index;
                }

                index++;
            }

            return emptyIndex;
        }
    }
}