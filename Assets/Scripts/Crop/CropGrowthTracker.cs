using System.Collections.Generic;
using KittyFarm.Service;
using KittyFarm.Time;
using UnityEngine;

namespace KittyFarm.CropSystem
{
    public class CropGrowthTracker : MonoBehaviour
    {
        private List<Crop> crops { get; } = new();

        private ITilemapService TilemapService => ServiceCenter.Get<ITilemapService>();

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
            var growthDetails = crop.GrowthDetails;
            crop.GrowthDetails.Status = GetCropCurrentStatus(growthDetails);
            
            if (growthDetails.Status != CropStatus.Healthy) return;
            
            crop.UpdateCurrentStage(GetCropCurrentStage(crop));
        }

        private CropStatus GetCropCurrentStatus(CropGrowthDetails growthDetails)
        {
            TilemapService.TryGetTileDetailsOn(growthDetails.CellPosition, out var tileDetails);
            return tileDetails.WettingValue <= 0 ? CropStatus.WaterLacked : CropStatus.Healthy;
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