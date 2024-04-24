using System.Collections.Generic;
using KittyFarm.Service;
using KittyFarm.Time;
using UnityEngine;

namespace KittyFarm.CropSystem
{
    public class CropGrowthTracker : MonoBehaviour
    {
        private List<Crop> crops { get; } = new();

        private ITilemapService tilemapService;

        private void OnEnable()
        {
            TimeManager.SecondPassed += UpdateAllCrops;
        }

        private void OnDisable()
        {
            TimeManager.SecondPassed += UpdateAllCrops;
        }

        private void Start()
        {
            tilemapService = ServiceCenter.Get<ITilemapService>();
            
            UpdateAllCrops();
        }

        private void UpdateAllCrops()
        {
            foreach (var crop in crops)
            {
                UpdateSingleCrop(crop);
            }
        }

        public void UpdateSingleCrop(Crop crop)
        {
            crop.UpdateCurrentStage(GetCropCurrentStage(crop));
        }

        private int GetCropCurrentStage(Crop crop)
        {
            var stageIndex = 0;
            var cropData = crop.Data;

            var growthDuration = TimeManager.GetTimeSpanFrom(crop.GrowthDetails.PlantedTime).TotalHours;
            // print($"{TimeManager.Instance.CurrentTime}, {growthDetails.PlantedTime}");

            foreach (var stage in cropData.Stages)
            {
                if (growthDuration >= stage.GrowthDuration)
                {
                    stageIndex++;
                    growthDuration -= stage.GrowthDuration;
                }
            }

            return Mathf.Min(stageIndex, cropData.Stages.Length - 1);
        }

        public void AddCrop(Crop crop)
        {
            crops.Add(crop);
        }

        public void RemoveCrop(Crop crop)
        {
            crops.Remove(crop);
        }
    }
}