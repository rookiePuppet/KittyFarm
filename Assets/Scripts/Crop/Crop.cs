using System;
using KittyFarm.Service;
using KittyFarm.Time;
using KittyFarm.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace KittyFarm.CropSystem
{
    public class Crop : MonoBehaviour, IPointerClickHandler
    {
        private CropDataSO Data => ServiceCenter.Get<ICropService>().CropDatabase.GetCropData(GrowthDetails.DataId);
        private CropInfo Info
        {
            get
            {
                var growthTime = TimeManager.Instance.CurrentTime - GrowthDetails.PlantedTime;
                // print($"{growthTime.TotalMinutes}, {Data.TotalMinutesToBeRipe}");
                var info = new CropInfo
                {
                    CropName = Data.CropName,
                    Stage = GrowthDetails.CurrentStage,
                    GrowthTime = growthTime,
                    RipeRate = (float)growthTime.TotalMinutes / Data.TotalMinutesToBeRipe
                };
                return info;
            }
        }

        public CropGrowthDetails GrowthDetails { get; private set; }

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

        public void OnPointerClick(PointerEventData eventData)
        {
            UIManager.Instance.GetUI<GameView>().ShowCropInfoBoard(Info);

            var cropService = ServiceCenter.Get<ICropService>();

            if (cropService.IsCropRipe(GrowthDetails))
            {
                var amount = cropService.HarvestCrop(this);
                print($"收获了{amount}个{Data.CropName}");

                Destroy(gameObject);
            }
        }
    }
}