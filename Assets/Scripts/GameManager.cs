using System;
using Framework;
using KittyFarm.UI;
using UnityEditor;
using Application = UnityEngine.Device.Application;

namespace KittyFarm
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public static event Action BeforeGameExit;
        
        private void OnApplicationQuit()
        {
            BeforeGameExit?.Invoke();
        }

        public void ExitGame()
        {
            BeforeGameExit?.Invoke();
            
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
        
#if UNITY_EDITOR
        public bool autoLoadScene = true;
        public SceneAsset defaultMap;

        private void Start()
        {
            if (autoLoadScene)
            {
                SceneLoader.Instance.LoadMapScene(defaultMap.name, () =>
                {
                    UIManager.Instance.ShowUI<GameView>();
                    UIManager.Instance.ShowUI<OnScreenControllerView>();
                });
            }
        }

#endif
    }
}