using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace KittyFarm.UI
{
    public class StartView : UIBase
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button exitButton;

        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private float textTypingDuration = 1f;
        [SerializeField] private float fadeAlpha = 0.4f;
        [SerializeField] private float fadeOutDuration = 1.2f;
        [SerializeField] private float fadeInDuration = 0.8f;

        private void Start()
        {
            startButton.onClick.AddListener(OnStartButtonClicked);
            settingsButton.onClick.AddListener(OnSettingsButtonClicked);
            exitButton.onClick.AddListener(OnExitButtonClicked);
            
            StartCoroutine(PlayTitleTextAnimationRoutine());
        }

        private IEnumerator PlayTitleTextAnimationRoutine()
        {
            var text = titleText.text;
            yield return DOTween.To(() => string.Empty, value => titleText.text = value, text, textTypingDuration)
                .SetEase(Ease.InOutSine)
                .WaitForCompletion();
            
            while (true)
            {
                var fadeOut = DOTween.To(() => titleText.alpha, value => titleText.alpha = value, fadeAlpha, fadeOutDuration);
                var fadeIn = DOTween.To(() => titleText.alpha, value => titleText.alpha = value, 1f, fadeInDuration);
                var sequence = DOTween.Sequence()
                    .Append(fadeOut)
                    .Append(fadeIn);
                yield return sequence.WaitForCompletion();
            }
        }

        private void OnStartButtonClicked()
        {
            SceneLoader.Instance.LoadMapScene("Plain", () =>
            {
                UIManager.Instance.HideUI<StartView>();

                UIManager.Instance.ShowUI<GameView>();
                UIManager.Instance.ShowUI<OnScreenControllerView>();
            });
            SceneManager.UnloadSceneAsync("StartScene");
        }

        private void OnSettingsButtonClicked()
        {
        }

        private void OnExitButtonClicked()
        {
            GameManager.Instance.ExitGame();
        }
    }
}