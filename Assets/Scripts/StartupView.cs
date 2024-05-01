using System;
using System.Threading.Tasks;
using DG.Tweening;
using KittyFarm.Service;
using KittyFarm.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace KittyFarm
{
    public class StartupView : UIBase
    {
        [SerializeField] private Image progressImage;
        [SerializeField] private TextMeshProUGUI progressText;

        // 范围0~100
        private int progress;

        public async void StartLoading(Action<Scene> setCurrentScene)
        {
            progress = 0;
            progressImage.fillAmount = 0;
            await UpdateProgressAsync(30, 0.2f);

            var startScene = await SceneLoader.LoadSceneAsync("StartScene");
            setCurrentScene?.Invoke(startScene);
            await UpdateProgressAsync(60, 0.5f);

            UIManager.Instance.ShowUI<StartView>();
            ServiceCenter.Get<ICameraService>().EnableFixedCamera();
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
    }
}