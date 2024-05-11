using System;
using System.Threading;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace KittyFarm.UI
{
    public class TalkBubble : MonoBehaviour
    {
        [SerializeField] private RectTransform bubble;
        [SerializeField] private float remainDuration = 1f;
        [SerializeField] private float typeContentDuration = 0.5f;

        private TextMeshProUGUI contentText;
        private CancellationTokenSource cts;

        private void Awake()
        {
            contentText = bubble.GetComponentInChildren<TextMeshProUGUI>();
        }

        public async void Show(string content)
        {
            cts?.Cancel();
            cts = new CancellationTokenSource();

            try
            {
                SetVisible(true);
                await TypeContentAsync(content);
                await Task.Delay((int)TimeSpan.FromSeconds(remainDuration).TotalMilliseconds, cts.Token);

                SetVisible(false);
            }
            catch (TaskCanceledException)
            {
            }
        }

        private Task TypeContentAsync(string content)
        {
            var source = new TaskCompletionSource<bool>();

            contentText.text = string.Empty;
            DOTween.To(() => contentText.text, value => contentText.text = value, content, typeContentDuration)
                .onComplete += () => { source.SetResult(true); };

            return source.Task;
        }

        private void SetVisible(bool visible)
        {
            bubble.gameObject.SetActive(visible);
        }
    }
}