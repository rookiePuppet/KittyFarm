using System.Collections.Generic;
using KittyFarm.Data;
using KittyFarm.Service;
using UnityEngine;

namespace KittyFarm.MapClick
{
    public class UsableItemSet
    {
        public ItemDataSO ItemData { get; private set; }
        public Vector3 WorldPosition { get; private set; }
        public Vector3Int CellPosition { get; private set; }
        public Vector3 CellCenterPosition => TilemapService.GetCellCenterWorld(CellPosition);

        public ITilemapService TilemapService => ServiceCenter.Get<ITilemapService>();
        public ICropService CropService => ServiceCenter.Get<ICropService>();
        public IItemService ItemService => ServiceCenter.Get<IItemService>();

        private readonly Dictionary<ItemType, UsableItem> itemDic = new();

        public UsableItem TakeUsableItem(ItemDataSO itemData, Vector3 worldPosition, Vector3Int cellPosition)
        {
            ItemData = itemData;
            WorldPosition = worldPosition;
            CellPosition = cellPosition;

            var itemType = itemData.Type;
            if (itemDic.TryGetValue(itemType, out var usableItem)) return usableItem;

            usableItem = InstantiateUsableItem(itemType);
            itemDic[itemType] = usableItem;

            return usableItem;
        }

        public bool MeetDistanceAtWorld =>
            Vector2.Distance(GameManager.Player.transform.position, WorldPosition) <= GetOperationRange(ItemData.Type);

        public bool MeetDistanceAtCellCenter =>
            Vector2.Distance(GameManager.Player.transform.position, CellCenterPosition) <= GetOperationRange(ItemData.Type);

        private static  float GetOperationRange(ItemType itemType) => itemType switch
        {
            ItemType.Seed => 1f,
            ItemType.FarmProduct => 2f,
            ItemType.Hoe => 1f,
            ItemType.HarvestTool => 1f,
            _ => 100f
        };
        
        private UsableItem InstantiateUsableItem(ItemType itemType) => itemType switch
        {
            ItemType.Seed => new Seed(this),
            ItemType.FarmProduct => new FarmProduct(this),
            ItemType.Hoe => new Hoe(this),
            ItemType.HarvestTool => new HarvestTool(this),
            _ => new UndefinedUsageItem(this)
        };
    }
}