using System;
using Framework;
using UnityEditor;
using Application = UnityEngine.Device.Application;

namespace KittyFarm
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public static event Action BeforeGameExit;

        public int CurrentMapId { get; private set; }

        private void OnEnable()
        {
            SceneLoader.MapLoaded += OnMapLoaded;
        }

        private void OnDisable()
        {
            SceneLoader.MapLoaded -= OnMapLoaded;
        }

        private void OnMapLoaded(int mapId) => CurrentMapId = mapId;

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