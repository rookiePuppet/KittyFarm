using System;
using KittyFarm.Data;
using KittyFarm.Service;
using KittyFarm.UI;
using UnityEngine;

namespace KittyFarm.Harvestable
{
    public class Crop : MonoBehaviour, IHarvestable
    {
        [SerializeField] private GameObject exclamationMark;

        public bool CanBeHarvested => ServiceCenter.Get<ICropService>().IsCropRipe(GrowthDetails);
        public CropGrowthDetails GrowthDetails { get; private set; }
        public CropDataSO Data => data;
        private CropDataSO data;

        private bool IsFinalStage => GrowthDetails.CurrentStage == Data.Stages.Length - 1;

        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Initialize(CropGrowthDetails details)
        {
            GrowthDetails = details;
            data = ServiceCenter.Get<ICropService>().CropDatabase.GetCropData(GrowthDetails.CropId);
            UpdateCropVisual();
        }

        public void Harvest()
        {
            var harvestCount = ServiceCenter.Get<ICropService>().HarvestCrop(this);
            UIManager.Instance.ShowUI<GetItemView>().Initialize(Data.ProductData, harvestCount);
            AudioManager.Instance.PlaySoundEffect(GameSoundEffect.HarvestCrop);
        }

        public void UpdateCurrentStage(int newStage)
        {
            var lastStage = GrowthDetails.CurrentStage;
            GrowthDetails.CurrentStage = newStage;

            if (lastStage != newStage)
            {
                UpdateCropVisual();
            }

            exclamationMark?.SetActive(IsFinalStage && CanBeHarvested);
        }

        private void UpdateCropVisual()
        {
            spriteRenderer.sprite = Data.Stages[GrowthDetails.CurrentStage].Sprite;
        }
    }
}