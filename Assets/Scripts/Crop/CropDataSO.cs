using System.Linq;
using KittyFarm.InventorySystem;
using UnityEngine;

namespace KittyFarm.CropSystem
{
    [CreateAssetMenu(fileName = "NewCropData", menuName = "Crop/Crop Data")]
    public class CropDataSO : ScriptableObject
    {
        public int Id;
        public string CropName;
        public FarmProductDataSO ProductData;
        public CropGrowthStage[] Stages;

        public int TotalMinutesToBeRipe => Stages.Sum((stage) => stage.GrowthDuration * 60);
    }
}