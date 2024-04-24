using System;
using Framework;
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
    }
}