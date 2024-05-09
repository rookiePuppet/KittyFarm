using UnityEngine;

namespace KittyFarm.Data
{
    [CreateAssetMenu(fileName = "GameAudioConfig", menuName = "Game Audio Config")]
    public class GameAudioConfigSO : ScriptableObject
    {
        [Header("音乐")]
        public AudioClip backgroundMusic;
        [Header("音效")]
        public AudioClip buttonClickSound;
        public AudioClip bagItemClickSound;
        public AudioClip commodityItemClickSound;
        public AudioClip switchSound;
        public AudioClip startGameSound;
        [Space]
        public AudioClip digSound;
        public AudioClip pickUpItemSound;
        public AudioClip coinSound;
    }
}