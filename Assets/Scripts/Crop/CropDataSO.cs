using KittyFarm.InventorySystem;
using UnityEngine;

namespace KittyFarm.CropSystem
{
    [CreateAssetMenu(fileName = "NewCropData", menuName = "Crop/Crop Data")]
    public class CropDataSO : ScriptableObject
    {
        public string CropName;
        public FarmProductDataSO ProductData;
        public CropGrowthStage[] Stages;
    }
}