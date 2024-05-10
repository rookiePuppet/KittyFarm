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
        public int MinHarvestCount = 1;
        public int MaxHarvestCount = 2;

        public int TotalMinutesToBeRipe => Stages.Sum(stage => stage.GrowthHours * 60 + stage.GrowthMinutes);

        public int RandomHarvestCount => Random.Range(MinHarvestCount, MaxHarvestCount + 1);
    }
}