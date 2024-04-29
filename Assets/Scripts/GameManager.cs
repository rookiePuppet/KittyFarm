using System;
using KittyFarm.UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Application = UnityEngine.Device.Application;

namespace KittyFarm
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject playerPrefab;

        public static event Action MapChanged;
        public static event Action BeforeGameExit;

        private static PlayerController player;
        private static Scene currentScene;

        private static bool IsPlayerEnabled
        {
            set => player.gameObject.SetActive(value);
        }

#if UNITY_EDITOR
        public bool autoLoadScene = true;
        public SceneAsset defaultScene;
#endif

        private void Start()
        {
            Application.targetFrameRate = 60;

            InitializePlayer();
            HandleSceneLoading();
        }

        private void OnApplicationQuit()
        {
            BeforeGameExit?.Invoke();
        }

        public static async void LoadMapScene()
        {
            SceneManager.UnloadSceneAsync(currentScene);

            var scene = await SceneLoader.LoadSceneAsync("Plain");
            currentScene = scene;

            UIManager.Instance.ClearCache();

            MapChanged?.Invoke();

            UIManager.Instance.ShowUI<GameView>();
            UIManager.Instance.ShowUI<OnScreenControllerView>();

            IsPlayerEnabled = true;
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
            player = FindObjectOfType<PlayerController>();
            if (player == null)
            {
                player = Instantiate(playerPrefab).GetComponent<PlayerController>();
            }

            IsPlayerEnabled = false;
        }

        private async void HandleSceneLoading()
        {
            if (!autoLoadScene)
            {
                currentScene = await SceneLoader.LoadSceneAndSetActiveAsync("StartScene");
                UIManager.Instance.ShowUI<StartView>();
            }

#if UNITY_EDITOR
            if (!autoLoadScene) return;

            var scene = SceneManager.GetSceneByName(defaultScene.name);
            if (scene.IsValid() && SceneManager.GetActiveScene() != scene)
            {
                SceneManager.SetActiveScene(scene);
            }

            currentScene = scene;
            LoadMapScene();
#endif
        }
    }
}