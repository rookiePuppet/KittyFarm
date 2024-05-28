using System.Collections.Generic;
using KittyFarm.Data;
using UnityEngine;

namespace KittyFarm.MapClick
{
    public static  class UsableItemSet
    {
        private static readonly Dictionary<ItemType, UsableItem> itemDic = new();

        public static UsableItem TakeUsableItem(ItemDataSO itemData, Vector3 worldPosition, Vector3Int cellPosition)
        {
            var itemType = itemData != null ? itemData.Type: ItemType.None;
            if (itemDic.TryGetValue(itemType, out var usableItem))
            {
                usableItem.Initialize(itemData, worldPosition, cellPosition);
                return usableItem;
            }

            usableItem = InstantiateUsableItem(itemType);
            itemDic[itemType] = usableItem;
            usableItem.Initialize(itemData, worldPosition, cellPosition);
            
            return usableItem;
        }
        
        public static bool MeetDistanceAt(Vector3 position, ItemType itemType) =>
            Vector2.Distance(GameManager.Player.transform.position, position) <= GetOperationRange(itemType);

        private static  float GetOperationRange(ItemType itemType) => itemType switch
        {
            ItemType.Seed => 1f,
            ItemType.FarmProduct => 2f,
            ItemType.Hoe => 1f,
            ItemType.Axe => 1.5f,
            ItemType.Basket => 1.5f,
            _ => 1f
        };
        
        private static UsableItem InstantiateUsableItem(ItemType itemType) => itemType switch
        {
            ItemType.Seed => new Seed(),
            ItemType.FarmProduct => new FarmProduct(),
            ItemType.Hoe => new Hoe(),
            ItemType.Axe => new Axe(),
            ItemType.Basket => new Basket(),
            _ => new Hand()
        };
    }
}