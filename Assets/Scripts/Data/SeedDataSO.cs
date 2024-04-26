using UnityEngine;

namespace KittyFarm.Data
{
    [CreateAssetMenu(fileName = "NewSeedData", menuName = "Data/Seed Data")]
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