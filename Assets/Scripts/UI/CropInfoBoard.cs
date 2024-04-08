using DG.Tweening;
using KittyFarm.CropSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KittyFarm.UI
{
    public class CropInfoBoard : UIBase
    {
        [SerializeField] private TextMeshProUGUI cropNameText;
        [SerializeField] private TextMeshProUGUI stageText;
        [SerializeField] private Image ripeRateProgressImage;
        [SerializeField] private TextMeshProUGUI growthTimeText;
        [SerializeField] private TextMeshProUGUI statusText;

        private RectTransform RectTransform => (RectTransform)transform;

        public override void Show()
        {
            var isVisibleBefore = IsVisible;
            
            base.Show();

            if (isVisibleBefore) return;
            
            var position = RectTransform.anchoredPosition;
            RectTransform.anchoredPosition = new Vector2(RectTransform.rect.width, position.y);
            RectTransform.DOAnchorPosX(0, 0.3f);
        }

        public override void Hide()
        {
            var tween = RectTransform.DOAnchorPosX(RectTransform.rect.width, 0.2f);
            tween.onComplete += () => base.Hide();
        }

        public void Refresh(CropInfo info)
        {
            cropNameText.text = info.CropName;
            stageText.text = $"{info.Stage}";
            ripeRateProgressImage.fillAmount = info.RipeRate;
            growthTimeText.text = $"{info.GrowthTime.Days}天{info.GrowthTime.Hours}时";

            statusText.text = "正常";
        }
    }
}