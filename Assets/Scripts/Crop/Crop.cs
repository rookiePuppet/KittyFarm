using UnityEngine;

namespace KittyFarm.CropSystem
{
    public class Crop : MonoBehaviour
    {
        public CropDataSO Data => GrowthDetails.Data;

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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CurrentStage++;
                if (CurrentStage >= Data.Stages.Length)
                {
                    CurrentStage = 0;
                }

                UpdateCropVisual();
            }
        }

        /*[SerializeField] private CropDataSO data;
        private void OnDrawGizmos()
        {
            if (data == null) return;
            spriteRenderer ??= GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = data.StageSprites[0];
        }*/
    }
}