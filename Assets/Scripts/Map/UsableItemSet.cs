using System.Collections.Generic;
using KittyFarm.Data;
using KittyFarm.Service;
using UnityEngine;

namespace KittyFarm.Map
{
    public class UsableItemSet
    {
        public ItemDataSO ItemData { get; private set; }
        public Vector3 WorldPosition { get; private set; }
        public Vector3Int CellPosition { get; private set; }
        public Vector3 CellCenterPosition { get; private set; }

        public readonly ITilemapService TilemapService = ServiceCenter.Get<ITilemapService>();
        public readonly ICropService CropService = ServiceCenter.Get<ICropService>();
        public readonly IItemService ItemService = ServiceCenter.Get<IItemService>();

        private readonly Dictionary<ItemType, UsableItem> itemDic = new();

        public UsableItem GetUsableItem(ItemDataSO itemData, Vector3 worldPosition, Vector3Int cellPosition)
        {
            ItemData = itemData;
            WorldPosition = worldPosition;
            CellPosition = cellPosition;
            CellCenterPosition = TilemapService.GetCellCenterWorld(cellPosition);

            var itemType = itemData.Type;
            if (itemDic.TryGetValue(itemType, out var usableItem)) return usableItem;

            usableItem = InstantiateUsableItem(itemType);
            itemDic[itemType] = usableItem;

            return usableItem;
        }

        public bool MeetDistanceAtWorld =>
            Vector2.Distance(GameManager.Player.transform.position, WorldPosition) <= ItemData.OperationRange;

        public bool MeetDistanceAtCellCenter =>
            Vector2.Distance(GameManager.Player.transform.position, CellCenterPosition) <= ItemData.OperationRange;

        private UsableItem InstantiateUsableItem(ItemType itemType) => itemType switch
        {
            ItemType.Seed => new Seed(this),
            ItemType.FarmProduct => new FarmProduct(this),
            ItemType.Hoe => new Hoe(this),
            _ => new UndefinedUsageItem(this)
        };
    }
}