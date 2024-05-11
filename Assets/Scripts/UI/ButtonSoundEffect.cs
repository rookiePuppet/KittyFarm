using UnityEngine;
using UnityEngine.UI;

namespace KittyFarm.UI
{
    public class ButtonSoundEffect : MonoBehaviour
    {
        [SerializeField] private GameSoundEffect soundEffect;
    
        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            button.onClick.AddListener(PlaySoundEffect);
        }
    
        private void OnDisable()
        {   
            button.onClick.RemoveListener(PlaySoundEffect);
        }

        private void PlaySoundEffect()
        {
            AudioManager.Instance.PlaySoundEffect(soundEffect);
        }
    }
}