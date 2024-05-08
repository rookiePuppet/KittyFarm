using UnityEngine;

namespace KittyFarm.Data
{
    public class SettingsDataSO : ScriptableObject
    {
        public const string PersistentDataName = "SettingsData";

        [SerializeField] private float musicVolume = 0.75f;
        [SerializeField] private float effectVolume = 0.75f;
        [SerializeField] private bool isMusicOn = true;
        [SerializeField] private bool isSoundEffectOn = true;

        public float MusicVolume
        {
            get => musicVolume;
            set => musicVolume = value;
        }
        public float EffectVolume
        {
            get => effectVolume;
            set => effectVolume = value;
        }
        public bool IsMusicOn
        {
            get => isMusicOn;
            set => isMusicOn = value;
        }
        public bool IsSoundEffectOn
        {
            get => isSoundEffectOn;
            set => isSoundEffectOn = value;
        }
        //
        // public AudioSettings AudioSettings
        // {
        //     get => new()
        //     {
        //         MusicVolume = musicVolume,
        //         EffectVolume = effectVolume,
        //         IsMusicOn = isMusicOn,
        //         IsSoundEffectOn = isSoundEffectOn
        //     };
        //     set
        //     {
        //         musicVolume = value.MusicVolume;
        //         effectVolume = value.EffectVolume;
        //         isMusicOn = value.IsMusicOn;
        //         isSoundEffectOn = value.IsSoundEffectOn;
        //     }
        // }
    }
}