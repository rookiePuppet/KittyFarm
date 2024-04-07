using KittyFarm.CropSystem;
using UnityEngine;

namespace KittyFarm.InventorySystem
{
    [CreateAssetMenu(fileName = "NewSeedData", menuName = "Inventory/Seed Data")]
    public class SeedDataSO : ItemDataSO
    {
        public CropDataSO CropData;
        
        protected override void OnValidate()
        {
            Type = ItemType.Seed;
            base.OnValidate();
        }
    }
}