using KittyFarm.Data;
using KittyFarm.Harvestable;
using KittyFarm.InventorySystem;
using KittyFarm.MapClick;
using UnityEngine;

namespace KittyFarm.Service
{
    public interface IItemService: IService
    {
        public ItemDatabaseSO ItemDatabase { get; }
        public Item SpawnItemAt(Vector3 position, ItemDataSO itemData, int amount = 1);
        public Item SpawnItemAt(Transform parent, Vector3 position, ItemDataSO itemData, int amount = 1);
        public void RemoveMapItem(Vector3 position);
        public UsableItem TakeUsableItem(ItemDataSO itemData, Vector3 worldPosition, Vector3Int cellPosition);
        public UsableItem TakeHarvestTool(IHarvestable harvestable, Vector3 worldPosition);
    }
}