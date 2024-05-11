using KittyFarm.Data;
using UnityEngine;
using UnityEngine.Playables;

namespace KittyFarm
{
    public class GameInitializer : MonoBehaviour
    {
        private PlayableDirector director;

        private void Awake()
        {
            director = GetComponent<PlayableDirector>();
        }

        private void Start()
        {
            if (GameDataCenter.Instance.SettingsData.IsNewPlayer)
            {
                director.Play();
                director.stopped += _ => { InitializeGame(); };
                
                InputReader.DisableInput();
                GameDataCenter.Instance.SettingsData.IsNewPlayer = false;
                
                return;
            }
            
            InitializeGame();
        }

        private void InitializeGame()
        {
            
            Destroy(gameObject);
        }
    }
}