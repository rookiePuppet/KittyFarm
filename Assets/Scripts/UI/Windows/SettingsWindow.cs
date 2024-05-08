using UnityEngine;
using UnityEngine.UI;

namespace KittyFarm.UI
{
    public class SettingsWindow : UIBase
    {
        [SerializeField] private SwitchController musicSwitch;
        [SerializeField] private SliderController musicSlider;
        [SerializeField] private SwitchController soundEffectSwitch;
        [SerializeField] private SliderController soundEffectSlider;
        [SerializeField] private Button closeButton;

        private void OnEnable()
        {
            musicSwitch.OnSwitch += OnSwitchMusic;
            musicSlider.OnValueChanged += OnMusicSliderValueChanged;
            soundEffectSwitch.OnSwitch += OnSwitchSoundEffect;
            soundEffectSlider.OnValueChanged += OnSoundEffectSliderValueChanged;

            closeButton.onClick.AddListener(Hide);
        }

        private void OnDisable()
        {
            musicSwitch.OnSwitch -= OnSwitchMusic;
            musicSlider.OnValueChanged -= OnMusicSliderValueChanged;
            soundEffectSwitch.OnSwitch -= OnSwitchSoundEffect;
            soundEffectSlider.OnValueChanged -= OnSoundEffectSliderValueChanged;

            closeButton.onClick.RemoveListener(Hide);
        }

        private void Start()
        {
            musicSwitch.IsOn = AudioManager.Instance.IsMusicOn;
            musicSlider.Value = AudioManager.Instance.MusicVolume;
            soundEffectSwitch.IsOn = AudioManager.Instance.IsSoundEffectOn;
            soundEffectSlider.Value = AudioManager.Instance.EffectVolume;
        }

        private void OnSwitchMusic(bool isOn)
        {
            AudioManager.Instance.IsMusicOn = isOn;
        }

        private void OnMusicSliderValueChanged(float value)
        {
            AudioManager.Instance.MusicVolume = value;
        }

        private void OnSwitchSoundEffect(bool isOn)
        {
            AudioManager.Instance.IsSoundEffectOn = isOn;
        }

        private void OnSoundEffectSliderValueChanged(float value)
        {
            AudioManager.Instance.EffectVolume = value;
        }
    }
}