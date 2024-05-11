using System;
using Framework;
using KittyFarm.Data;
using KittyFarm.UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Application = UnityEngine.Device.Application;

namespace KittyFarm
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public static event Action MapChanged;
        public static event Action BeforeGameExit;

        [SerializeField] private Camera mainCamera; // Main场景中的摄像机

        public static PlayerController Player { get; private set; }
        
        public static bool IsPlayerEnabled
        {
            set => Player.gameObject.SetActive(value);
        }

        private static Scene currentScene;

        private async void Start()
        {
            Application.targetFrameRate = 60;
            
            var startupView = UIManager.Instance.ShowUI<StartupView>();
            await startupView.StartLoading(startScene => currentScene = startScene);

            mainCamera.enabled = false;
            AudioManager.Instance.PlayBackgroundMusic();
        }

        private void OnApplicationQuit()
        {
            BeforeGameExit?.Invoke();
        }

        public static async void LoadMapScene()
        {
            var lastScene = currentScene;
            currentScene = await SceneLoader.LoadSceneAsync(SceneName.Plain);
            
            Player = FindObjectOfType<PlayerController>();
            Player.transform.position = GameDataCenter.Instance.PlayerData.LastPosition;
            
            await SceneLoader.UnityUnloadSceneAsync(lastScene);
            
            MapChanged?.Invoke();
            
            UIManager.Instance.ClearCache();
            UIManager.Instance.ShowUI<OnScreenControllerView>();
            UIManager.Instance.ShowUI<GameView>(UILayer.Bottom);
        }

        public static async void BackToStartScene()
        {
            BeforeGameExit?.Invoke();

            var lastScene = currentScene;
            currentScene = await SceneLoader.LoadSceneAsync(SceneName.Start);
            await SceneLoader.UnityUnloadSceneAsync(lastScene);
            
            UIManager.Instance.ClearCache();
            UIManager.Instance.ShowUI<StartView>(UILayer.Bottom);
        }

        public static void ExitGame()
        {
            BeforeGameExit?.Invoke();

#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }
}