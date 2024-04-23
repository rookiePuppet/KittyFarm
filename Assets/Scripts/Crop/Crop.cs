using KittyFarm.Service;
using KittyFarm.Time;
using UnityEngine;

namespace KittyFarm.CropSystem
{
    public class Crop : MonoBehaviour
    {
        public CropGrowthDetails GrowthDetails { get; private set; }
        public CropInfo Info
        {
            get
            {
                cropService.GrowthTracker.UpdateSingleCrop(this);
                
                var growthTime = TimeManager.GetTimeSpanFrom(GrowthDetails.PlantedTime);
                // print($"{growthTime.TotalMinutes}, {Data.TotalMinutesToBeRipe}");

                cropInfo.CropName = Data.CropName;
                cropInfo.Stage = GrowthDetails.CurrentStage;
                cropInfo.GrowthTime = growthTime;
                cropInfo.RipeRate = (float)growthTime.TotalMinutes / Data.TotalMinutesToBeRipe;
                cropInfo.CropStatus = GrowthDetails.Status;
                
                return cropInfo;
            }
        }

        private CropInfo cropInfo;
        
        public CropDataSO Data => cropService.CropDatabase.GetCropData(GrowthDetails.CropId);

        private SpriteRenderer spriteRenderer;

        private ICropService cropService;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            
            cropService = ServiceCenter.Get<ICropService>();
        }

        public void Initialize(CropGrowthDetails details)
        {
            GrowthDetails = details;
            UpdateCropVisual();
        }

        public void UpdateCurrentStage(int newStage)
        {
            var lastStage = GrowthDetails.CurrentStage;
            
            GrowthDetails.CurrentStage = newStage;
            
            if (lastStage != newStage)
            {
                UpdateCropVisual();
            }
        }

        private void UpdateCropVisual()
        {
            spriteRenderer.sprite = Data.Stages[GrowthDetails.CurrentStage].Sprite;
        }
    }
}