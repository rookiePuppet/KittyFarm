using System;
using System.Collections.Generic;
using KittyFarm.InventorySystem;
using KittyFarm.UI;
using UnityEngine;

namespace KittyFarm.Data
{
    [Serializable]
    public class PlayerInventory
    {
        public static event Action<int, InventoryItem> ItemChanged;

        [SerializeField] private List<InventoryItem> items = new();

        public List<InventoryItem> Items => items;

        private const int MaxSize = 9;

        public PlayerInventory()
        {
            ShopWindow.PurchasedCommodity += OnPurchasedCommodity;
        }

        private void OnPurchasedCommodity(ItemDataSO itemData, int amount)
        {
            AddItem(itemData, amount);
        }

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

        public void RemoveItem(ItemDataSO itemData, int amount)
        {
            var index = items.FindIndex(item => item.itemId == itemData.Id);
            if (index == -1)
            {
                return;
            }

            var item = items[index];

            var totalCount = item.count;
            if (amount >= totalCount)
            {
                RemoveItemAll(index);
            }
            else
            {
                item.count -= amount;
                ItemChanged?.Invoke(index, item);
            }
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

        public void Initialize()
        {
            if (items.Count != 0) return;
            for (var index = 0; index < MaxSize; index++)
            {
                items.Add(new InventoryItem());
            }
        }
    }
}