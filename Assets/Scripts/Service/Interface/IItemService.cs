using KittyFarm.Data;
using KittyFarm.InventorySystem;
using UnityEngine;

namespace KittyFarm.Service
{
    public interface IItemService: IService
    {
        public ItemDatabaseSO ItemDatabase { get; }
        public Item SpawnItemAt(Vector3 position, ItemDataSO itemData, int amount = 1);
        public Item SpawnItemAt(Transform parent, Vector3 position, ItemDataSO itemData, int amount = 1);
    }
}