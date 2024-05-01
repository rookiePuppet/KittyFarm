using System.Linq;
using KittyFarm.CropSystem;
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
        public int MaxHarvestCount = 3;

        public int TotalMinutesToBeRipe => Stages.Sum(stage => stage.GrowthDuration * 60);

        public int RandomHarvestCount => Random.Range(1, MaxHarvestCount + 1);
    }
}