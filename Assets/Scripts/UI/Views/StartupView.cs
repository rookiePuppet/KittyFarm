using System;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace KittyFarm.UI
{
    public class StartupView : UIBase
    {
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private Image progressImage;
        [SerializeField] private TextMeshProUGUI progressText;

        // 范围0~100
        private int progress;

        public async Task StartLoading(Action<Scene> setCurrentScene)
        {
            await TypeTitleText();
            await UpdateProgressAsync(30, 0.1f);

            var startScene = await SceneLoader.LoadSceneAsync(SceneName.Start);
            setCurrentScene?.Invoke(startScene);
            await UpdateProgressAsync(85, 0.5f);
            
            AudioManager.Instance.Initialize();
            UIManager.Instance.ShowUI<StartView>(UILayer.Bottom);
            await UpdateProgressAsync(100, 0.2f);
            
            Hide();
        }

        private Task<bool> UpdateProgressAsync(int endValue, float duration)
        {
            var tcs = new TaskCompletionSource<bool>();

            var tween = DOTween.To(() => progress, value => progress = value, endValue, duration);
            tween.onComplete += () => { tcs.SetResult(true); };
            tween.onUpdate += () =>
            {
                progressImage.fillAmount = progress / 100f;
                progressText.text = $"{progress}%";
            };

            return tcs.Task;
        }

        private Task TypeTitleText()
        {
            var source = new TaskCompletionSource<bool>();

            var content = titleText.text;
            titleText.text = string.Empty;
            DOTween.To(() => titleText.text, value => titleText.text = value, content, 0.5f)
                .onComplete += () => { source.SetResult(true); };

            return source.Task;
        }
    }
}