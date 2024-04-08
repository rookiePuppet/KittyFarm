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
        private CropDataSO Data => GrowthDetails.Data;
        private CropInfo Info
        {
            get
            {
                var growthTime = TimeManager.Instance.CurrentTime - GrowthDetails.PlantedTime;
                print($"{growthTime.TotalMinutes}, {Data.TotalMinuteToBeRipe}");
                var info = new CropInfo
                {
                    CropName = Data.CropName,
                    Stage = GrowthDetails.CurrentStage,
                    GrowthTime = growthTime,
                    RipeRate = (float)growthTime.TotalMinutes / Data.TotalMinuteToBeRipe
                };
                return info;
            }
        }

        public CropGrowthDetails GrowthDetails { get; private set; }
        public int CurrentStage
        {
            get => GrowthDetails.CurrentStage;
            set
            {
                GrowthDetails.CurrentStage = value;
                UpdateCropVisual();
            }
        }

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

        private void UpdateCropVisual()
        {
            spriteRenderer.sprite = Data.Stages[CurrentStage].Sprite;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            UIManager.Instance.GetUI<GameView>().ShowCropInfoBoard(Info);

            if (GrowthDetails.IsRipe)
            {
                var amount = ServiceCenter.Get<ICropService>().HarvestCrop(this);
                print($"收获了{amount}个{Data.CropName}");

                Destroy(gameObject);
            }
        }
    }
}