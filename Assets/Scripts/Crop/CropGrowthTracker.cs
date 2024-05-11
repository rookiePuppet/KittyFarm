using System;
using System.Collections.Generic;
using KittyFarm.Time;
using UnityEngine;

namespace KittyFarm.CropSystem
{
    public class CropGrowthTracker : MonoBehaviour
    {
        private List<Crop> crops { get; } = new();

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
            var growthDuration = TimeManager.GetTimeSpanFrom(crop.GrowthDetails.PlantedTime).TotalMinutes;
            
            foreach (var stage in cropData.Stages)
            {
                var stageMinutes = new TimeSpan(stage.GrowthHours, stage.GrowthMinutes, 0).TotalMinutes;
                if (growthDuration >= stageMinutes)
                {
                    stageIndex++;
                    growthDuration -= stageMinutes;
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