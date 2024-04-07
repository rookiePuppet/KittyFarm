using System.Collections.Generic;
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

        private async void OnSecondPassed()
        {
            foreach (var crop in crops)
            {
                var stageIndex = GetCropCurrentStage(crop.GrowthDetails);
                if (stageIndex != crop.CurrentStage)
                {
                    crop.CurrentStage = stageIndex;
                }
                
                await Task.Yield();
            }
        }

        private int GetCropCurrentStage(CropGrowthDetails growthDetails)
        {
            var stageIndex = 0;

            var growthDuration = (TimeManager.Instance.CurrentTime - growthDetails.PlantedTime).Minutes;
            foreach (var stage in growthDetails.Data.Stages)
            {
                if (growthDuration >= stage.GrowthDuration)
                {
                    stageIndex++;
                    growthDuration -= stage.GrowthDuration;
                }
            }

            return Mathf.Min(stageIndex, growthDetails.Data.Stages.Length - 1);
        }

        public void AddCrop(Crop crop)
        {
            crops.Add(crop);
        }
    }
}