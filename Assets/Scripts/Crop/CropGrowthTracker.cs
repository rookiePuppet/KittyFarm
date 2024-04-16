using System;
using System.Collections.Generic;
using KittyFarm.Service;
using KittyFarm.Time;
using UnityEngine;
using Task = System.Threading.Tasks.Task;

namespace KittyFarm.CropSystem
{
    public class CropGrowthTracker : MonoBehaviour
    {
        private List<Crop> crops { get; } = new();

        private void OnEnable()
        {
            TimeManager.Instance.SecondPassed += OnSecondPassed;
        }

        private void OnDisable()
        {
            TimeManager.Instance.SecondPassed += OnSecondPassed;
        }

        private void OnSecondPassed()
        {
            foreach (var crop in crops)
            {
                var stageIndex = GetCropCurrentStage(crop.GrowthDetails);
                crop.UpdateCurrentStage(stageIndex);
            }
        }

        private int GetCropCurrentStage(CropGrowthDetails growthDetails)
        {
            var stageIndex = 0;

            var cropData = ServiceCenter.Get<ICropService>().CropDatabase.GetCropData(growthDetails.DataId);

            var growthDuration = (TimeManager.Instance.CurrentTime - growthDetails.PlantedTime).TotalHours;
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