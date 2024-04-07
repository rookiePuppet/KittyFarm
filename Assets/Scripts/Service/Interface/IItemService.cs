using KittyFarm.InventorySystem;
using UnityEngine;

namespace KittyFarm.Service
{
    public interface IItemService: IService
    {
        public Item SpawnItemAt(Vector3 position, ItemDataSO itemData, int amount = 1);
    }
}