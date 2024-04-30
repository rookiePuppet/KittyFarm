using System;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace KittyFarm.UI
{
    public class KittyTalkingBox : UIBase
    {
        [SerializeField] private TextMeshProUGUI contentText;
        [SerializeField] private float xOffset;

        public Vector3 Position
        {
            set => rectTransform.anchoredPosition = value;
        }
        
        private bool isWorking;

        private RectTransform rectTransform;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        public override void Show()
        {
            base.Show();

            rectTransform.DOAnchorPosX(rectTransform.anchoredPosition.x + xOffset, 1f);
            isWorking = true;
        }

        public override async void Hide()
        {
            base.Hide();

            await Task.Delay(1200);
            
            isWorking = false;
        }

        public void Talk(string content)
        {
            if (isWorking)
            {
                return;
            }

            Show();

            contentText.text = string.Empty;
            var tween = DOTween.To(() => contentText.text,
                value => contentText.text = value,
                content, 1f);

            tween.onComplete += Hide;
        }
    }
}