using UnityEngine;

namespace KittyFarm.Data
{
    [CreateAssetMenu(fileName = "GameAudioConfig", menuName = "Game Audio Config")]
    public class GameAudioConfigSO: ScriptableObject
    {
        [SerializeField] private AudioClip backgroundMusic;

        public AudioClip BackgroundMusic => backgroundMusic;
    }
}