using System;
using System.Collections.Generic;
using KittyFarm.Time;
using UnityEngine;

namespace KittyFarm.Harvestable
{
    public class CropGrowthTracker : MonoBehaviour
    {
        private List<Crop> crops { get; } = new();

        private void OnEnable()
        {
            TimeManager.SecondPassed += OnSecondPassed;
        }

        private void OnDisable()
        {
            TimeManager.SecondPassed -= OnSecondPassed;
        }

        private void Start()
        {
            foreach (var crop in crops)
            {
                UpdateSingleCrop(crop);
            }
        }

        private void OnSecondPassed()
        {
            if (!GameManager.IsInMap)
            {
                return;
            }

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
            var growthDuration = TimeManager.GetTimeSpanFrom(crop.GrowthDetails.PlantedTime).TotalMinutes;

            foreach (var stage in crop.Data.Stages)
            {
                var stageMinutes = new TimeSpan(stage.GrowthHours, stage.GrowthMinutes, 0).TotalMinutes;
                if (growthDuration >= stageMinutes)
                {
                    stageIndex++;
                    growthDuration -= stageMinutes;
                }
            }

            return Mathf.Min(stageIndex, crop.Data.Stages.Length - 1);
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