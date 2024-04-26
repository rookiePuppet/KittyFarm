using KittyFarm.Data;
using KittyFarm.Service;
using KittyFarm.Time;
using KittyFarm.UI;
using UnityEngine;

namespace KittyFarm.CropSystem
{
    public class Crop : MonoBehaviour
    {
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