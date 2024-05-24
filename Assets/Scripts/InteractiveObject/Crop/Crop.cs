using KittyFarm.Data;
using KittyFarm.Service;
using KittyFarm.UI;
using UnityEngine;

namespace KittyFarm.InteractiveObject
{
    public class Crop : MonoBehaviour, ICollectable
    {
        [SerializeField] private GameObject exclamationMark;

        public bool CanBeCollected => ServiceCenter.Get<ICropService>().IsCropRipe(GrowthDetails);
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

        public void UpdateCurrentStage(int newStage)
        {
            var lastStage = GrowthDetails.CurrentStage;
            GrowthDetails.CurrentStage = newStage;

            if (lastStage != newStage)
            {
                UpdateCropVisual();
            }

            exclamationMark?.SetActive(IsFinalStage && CanBeCollected);
        }

        private void UpdateCropVisual()
        {
            spriteRenderer.sprite = Data.Stages[GrowthDetails.CurrentStage].Sprite;
        }

        public void Collect()
        {
            var collectCount = ServiceCenter.Get<ICropService>().HarvestCrop(this);
            UIManager.Instance.ShowUI<GetItemView>().Initialize(Data.ProductData, collectCount);
            AudioManager.Instance.PlaySoundEffect(GameSoundEffect.HarvestCrop);
        }
    }
}