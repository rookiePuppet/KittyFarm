using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
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

        [SerializeField] private float buttonStretchSize = 1.2f;

        private RectTransform startButtonRectTransform;

        private bool playTextAnimation = true;
        private bool playStartButtonAnimation = true;

        private void Awake()
        {
            startButtonRectTransform = startButton.GetComponent<RectTransform>();
        }

        private void OnDisable()
        {
            playTextAnimation = false;
            playStartButtonAnimation = false;
        }

        private void Start()
        {
            startButton.onClick.AddListener(OnStartButtonClicked);
            settingsButton.onClick.AddListener(OnSettingsButtonClicked);
            exitButton.onClick.AddListener(OnExitButtonClicked);

            StartCoroutine(PlayTitleTextAnimationRoutine());
            StartCoroutine(PlayStartButtonAnimationRoutine());
        }

        private IEnumerator PlayStartButtonAnimationRoutine()
        {
            yield return startButtonRectTransform.DOScale(buttonStretchSize * Vector2.one, 1f);

            while (playStartButtonAnimation)
            {
                var sequence = DOTween.Sequence()
                    .Append(startButtonRectTransform.DOScale(1f, 1f))
                    .Append(startButtonRectTransform.DOScale(buttonStretchSize * Vector2.one, 1f));
                yield return sequence.WaitForCompletion();
            }
        }

        private IEnumerator PlayTitleTextAnimationRoutine()
        {
            var text = titleText.text;
            yield return DOTween.To(() => string.Empty, value => titleText.text = value, text, textTypingDuration)
                .SetEase(Ease.InOutSine)
                .WaitForCompletion();

            while (playTextAnimation)
            {
                var fadeOut = DOTween.To(() => titleText.alpha, value => titleText.alpha = value, fadeAlpha,
                    fadeOutDuration);
                var fadeIn = DOTween.To(() => titleText.alpha, value => titleText.alpha = value, 1f, fadeInDuration);
                var sequence = DOTween.Sequence()
                    .Append(fadeOut)
                    .Append(fadeIn);
                yield return sequence.WaitForCompletion();
            }
        }

        private void OnStartButtonClicked()
        {
            GameManager.LoadMapScene();
        }

        private void OnSettingsButtonClicked()
        {
        }

        private void OnExitButtonClicked()
        {
            GameManager.ExitGame();
        }
    }
}