using System;
using Framework;
using KittyFarm.Data;
using UnityEngine;

namespace KittyFarm
{
    public class AudioManager : MonoSingleton<AudioManager>
    {
        [SerializeField] private GameAudioConfigSO audioConfig;
        public GameAudioConfigSO AudioConfig => audioConfig;
        
        public float MusicVolume
        {
            get => SettingsData.MusicVolume;
            set
            {
                SettingsData.MusicVolume = value;
                backgroundMusicSource.volume = value;
            }
        }
        public float EffectVolume
        {
            get => SettingsData.EffectVolume;
            set
            {
                SettingsData.EffectVolume = value;
                soundEffectSource.volume = value;
            }
        }
        public bool IsMusicOn
        {
            get => SettingsData.IsMusicOn;
            set
            {
                SettingsData.IsMusicOn = value;
                backgroundMusicSource.mute = !value;
            }
        }
        public bool IsSoundEffectOn
        {
            get => SettingsData.IsSoundEffectOn;
            set
            {
                SettingsData.IsSoundEffectOn = value;
                soundEffectSource.mute = !value;
            }
        }

        private AudioSource backgroundMusicSource;
        private AudioSource soundEffectSource;

        private SettingsDataSO SettingsData => GameDataCenter.Instance.SettingsData;

        public void Initialize()
        {
            var obj = new GameObject("BackgroundMusic");
            obj.transform.parent = transform;
            backgroundMusicSource = obj.AddComponent<AudioSource>();

            obj = new GameObject("SoundEffect");
            obj.transform.parent = transform;
            soundEffectSource = obj.AddComponent<AudioSource>();

            MusicVolume = SettingsData.MusicVolume;
            EffectVolume = SettingsData.EffectVolume;
            IsMusicOn = SettingsData.IsMusicOn;
            IsSoundEffectOn = SettingsData.IsSoundEffectOn;
        }

        public void PlayMusic(AudioClip clip)
        {
            backgroundMusicSource.clip = clip;
            backgroundMusicSource.loop = true;
            backgroundMusicSource.volume = MusicVolume;

            backgroundMusicSource.Play();
        }

        public void PlayBackgroundMusic()
        {
            PlayMusic(audioConfig.backgroundMusic);
        }

        public void PlaySoundEffect(AudioClip clip)
        {
            soundEffectSource.clip = clip;
            soundEffectSource.volume = EffectVolume;
            soundEffectSource.Play();
        }

        public void PlaySoundEffect(GameSoundEffect soundEffect)
        {
            var clip = soundEffect switch
            {
                GameSoundEffect.ButtonClick => audioConfig.buttonClickSound,
                GameSoundEffect.BagItemClick => audioConfig.bagItemClickSound,
                GameSoundEffect.CommodityItemClick => audioConfig.commodityItemClickSound,
                GameSoundEffect.Dig => audioConfig.digSound,
                GameSoundEffect.PickUpItem => audioConfig.pickUpItemSound,
                GameSoundEffect.Switch => audioConfig.switchSound,
                GameSoundEffect.StartGame => audioConfig.startGameSound,
                _ => audioConfig.buttonClickSound
            };
            
            PlaySoundEffect(clip);
        }
    }
}