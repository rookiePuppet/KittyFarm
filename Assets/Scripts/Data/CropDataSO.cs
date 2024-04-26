using System.Linq;
using KittyFarm.CropSystem;
using KittyFarm.InventorySystem;
using UnityEngine;

namespace KittyFarm.Data
{
    [CreateAssetMenu(fileName = "NewCropData", menuName = "Data/Crop Data")]
    public class CropDataSO : ScriptableObject
    {
        public int Id;
        public string CropName;
        public FarmProductDataSO ProductData;
        public CropGrowthStage[] Stages;

        public int TotalMinutesToBeRipe => Stages.Sum((stage) => stage.GrowthDuration * 60);
    }
}