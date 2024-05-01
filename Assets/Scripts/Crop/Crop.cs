using KittyFarm.Data;
using KittyFarm.Service;
using UnityEngine;

namespace KittyFarm.CropSystem
{
    public class Crop : MonoBehaviour, IHarvestable
    {
        public bool CanBeHarvested => ServiceCenter.Get<ICropService>().IsCropRipe(GrowthDetails);
        public CropGrowthDetails GrowthDetails { get; private set; }
        public CropDataSO Data => ServiceCenter.Get<ICropService>().CropDatabase.GetCropData(GrowthDetails.CropId);
        
        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Initialize(CropGrowthDetails details)
        {
            GrowthDetails = details;
            UpdateCropVisual();
        }
        
        public void Harvest()
        {
            ServiceCenter.Get<ICropService>().HarvestCrop(this);
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