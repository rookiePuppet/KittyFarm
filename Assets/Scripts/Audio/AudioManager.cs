using System.Threading.Tasks;
using Framework;
using KittyFarm.Data;
using UnityEngine;
using UnityEngine.Pool;

namespace KittyFarm
{
    public class AudioManager : MonoSingleton<AudioManager>
    {
        [SerializeField] private GameAudioConfigSO audioConfig;

        [Tooltip("音效开始播放直到放回对象池的时间（毫秒）")]
        [SerializeField] private int soundEffectLifeTime = 1100;

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
        private IObjectPool<AudioSource> soundEffectPool;

        private SettingsDataSO SettingsData => GameDataCenter.Instance.SettingsData;

        public void Initialize()
        {
            var obj = new GameObject("BackgroundMusic");
            obj.transform.parent = transform;
            backgroundMusicSource = obj.AddComponent<AudioSource>();

            soundEffectPool = new ObjectPool<AudioSource>(createFunc: CreateAudioSource, actionOnGet: OnGetAudioSource,
                actionOnRelease: OnReleaseAudioSource);

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
            var soundEffectSource = soundEffectPool.Get();
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
                GameSoundEffect.Coin => audioConfig.coinSound,
                GameSoundEffect.TreeShake => audioConfig.treeShakeSound,
                GameSoundEffect.Door => audioConfig.doorSound,
                GameSoundEffect.PlantSeed => audioConfig.plantSeedSound,
                GameSoundEffect.HarvestCrop => audioConfig.harvestCropSound,
                _ => audioConfig.buttonClickSound
            };

            PlaySoundEffect(clip);
        }

        private AudioSource CreateAudioSource()
        {
            var obj = new GameObject("SoundEffect");
            obj.transform.parent = transform;
            return obj.AddComponent<AudioSource>();
        }

        private async void OnGetAudioSource(AudioSource source)
        {
            source.gameObject.SetActive(true);
            source.volume = EffectVolume;
            source.mute = !IsSoundEffectOn;
            await Task.Delay(soundEffectLifeTime);
            
            soundEffectPool.Release(source);
        }

        private void OnReleaseAudioSource(AudioSource source)
        {
            source.gameObject.SetActive(false);
        }
    }
}