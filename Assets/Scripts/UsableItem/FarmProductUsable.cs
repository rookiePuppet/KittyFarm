using KittyFarm.Data;
using KittyFarm.InventorySystem;
using KittyFarm.Service;
using UnityEngine;

namespace KittyFarm
{
    public class FarmProductUsable : IUsableItem
    {
        public bool CanUse { get; }
        private readonly ItemDataSO itemData;
        private readonly Vector3 worldPosition;
        private readonly Vector3Int cellPosition;
        
        public FarmProductUsable(Vector3 worldPosition, Vector3Int cellPosition, ItemDataSO itemData)
        {
            this.itemData = itemData;

            CanUse = true;
        }
        
        public void Use()
        {
            ServiceCenter.Get<IItemService>().SpawnItemAt(worldPosition, itemData);
        }
    }
}