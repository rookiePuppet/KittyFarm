using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

namespace KittyFarm.UI
{
    public class GameMessage : MonoBehaviour
    {
        [SerializeField] private float hideTweenDuration = 1f;
        [SerializeField] private float hidePosY = 25f;
        
        public IObjectPool<GameMessage> Pool { get; set; }
        
        private TextMeshProUGUI contentText;

        private void Awake()
        {
            contentText = GetComponent<TextMeshProUGUI>();
        }

        public async void Show(string content)
        {
            contentText.text = content;
            contentText.alpha = 1;
            contentText.rectTransform.anchoredPosition = Vector2.zero;
            
            await Task.Delay(300);
            
            Hide();
        }
        
        private void Hide()
        {
            var alphaTween = DOTween.To(() => contentText.alpha,
                value => contentText.alpha = value, 0, hideTweenDuration);
            contentText.rectTransform.DOAnchorPosY(hidePosY, hideTweenDuration);
            alphaTween.onComplete += OnTweenCompleted;
        }

        private async void OnTweenCompleted()
        {
            await Task.Delay(1000);
            
            Pool.Release(this);
        }
    }
}