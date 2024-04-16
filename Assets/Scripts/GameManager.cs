using System;
using Framework;
using UnityEngine;
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

            Application.Quit();
        }
    }
}