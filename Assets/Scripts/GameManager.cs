using System;
using Framework;
using KittyFarm.Data;
using KittyFarm.Service;
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
        
        [SerializeField] private GameObject playerPrefab;

        public static PlayerController Player { get; private set; }
        
        private static bool IsPlayerEnabled
        {
            set => Player.gameObject.SetActive(value);
        }

        private static Scene currentScene;

        private async void Start()
        {
            Application.targetFrameRate = 60;
            
            var startupView = UIManager.Instance.ShowUI<StartupView>();
            await startupView.StartLoading(startScene => currentScene = startScene);

            InitializePlayer();
            
            AudioManager.Instance.PlayBackgroundMusic();
        }

        private void OnApplicationQuit()
        {
            BeforeGameExit?.Invoke();
        }

        public static async void LoadMapScene()
        {
            var lastScene = currentScene;
            currentScene = await SceneLoader.LoadSceneAsync(SceneNameCollection.Plain);
            await SceneLoader.UnityUnloadSceneAsync(lastScene);
            
            MapChanged?.Invoke();
            UIManager.Instance.ClearCache();
            UIManager.Instance.ShowUI<GameView>(UILayer.Bottom);
            UIManager.Instance.ShowUI<OnScreenControllerView>(UILayer.Bottom);

            Player.transform.position = GameDataCenter.Instance.PlayerData.LastPosition;
            IsPlayerEnabled = true;

            ServiceCenter.Get<ICameraService>().EnableKineticCamera();
        }

        public static async void BackToStartScene()
        {
            BeforeGameExit?.Invoke();

            var lastScene = currentScene;
            currentScene = await SceneLoader.LoadSceneAsync(SceneNameCollection.Start);
            await SceneLoader.UnityUnloadSceneAsync(lastScene);
            
            UIManager.Instance.ClearCache();
            UIManager.Instance.ShowUI<StartView>(UILayer.Bottom);
            ServiceCenter.Get<ICameraService>().EnableFixedCamera();

            IsPlayerEnabled = false;
        }

        public static void ExitGame()
        {
            BeforeGameExit?.Invoke();

#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }

        private void InitializePlayer()
        {
            Player = FindObjectOfType<PlayerController>();
            if (Player == null)
            {
                Player = Instantiate(playerPrefab).GetComponent<PlayerController>();
            }

            IsPlayerEnabled = false;
        }
    }
}