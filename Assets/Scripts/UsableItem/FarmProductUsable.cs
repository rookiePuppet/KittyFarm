using KittyFarm.InventorySystem;
using KittyFarm.Service;
using UnityEngine;

namespace KittyFarm
{
    public class FarmProductUsable : IUsableItem
    {
        private readonly ItemDataSO itemData;
        
        public FarmProductUsable(ItemDataSO itemData)
        {
            this.itemData = itemData;
        }
        
        public void Use(Vector3 worldPosition, Vector3Int cellPosition)
        {
            ServiceCenter.Get<IItemService>().SpawnItemAt(worldPosition, itemData);
        }
    }
}