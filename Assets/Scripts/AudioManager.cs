using Framework;
using KittyFarm.Data;
using UnityEngine;

namespace KittyFarm
{
    public class AudioManager : MonoSingleton<AudioManager>
    {
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
            set => SettingsData.EffectVolume = value;
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
            set => SettingsData.IsSoundEffectOn = value;
        }

        private AudioSource backgroundMusicSource;

        private SettingsDataSO SettingsData => GameDataCenter.Instance.SettingsData;

        public void Initialize()
        {
            var obj = new GameObject("BackgroundMusic");
            obj.transform.parent = transform;

            backgroundMusicSource = obj.AddComponent<AudioSource>();
            
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

        public void PlaySoundEffect()
        {
        }
    }
}