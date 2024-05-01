using UnityEngine;

namespace KittyFarm.Data
{
    [CreateAssetMenu(fileName = "NewSeedData", menuName = "Data/Seed Data")]
    public class SeedDataSO : ItemDataSO
    {
        public CropDataSO CropData;
        
        protected void OnValidate()
        {
            Type = ItemType.Seed;
        }
    }
}