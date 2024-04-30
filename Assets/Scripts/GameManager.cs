using System;
using KittyFarm.Data;
using KittyFarm.Service;
using KittyFarm.UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Application = UnityEngine.Device.Application;

namespace KittyFarm
{
    public class GameManager : MonoBehaviour
    {
        public static event Action MapChanged;
        public static event Action BeforeGameExit;

        [SerializeField] private GameObject playerPrefab;

        private static bool IsPlayerEnabled
        {
            set => player.gameObject.SetActive(value);
        }

        private static PlayerController player;
        private static Scene currentScene;

        private void Start()
        {
            Application.targetFrameRate = 60;
            
            UIManager.Instance.ShowUI<StartView>();
            ServiceCenter.Get<ICameraService>().EnableFixedCamera();
            
            InitializePlayer();
        }

        private void OnApplicationQuit()
        {
            BeforeGameExit?.Invoke();
        }

        public static async void LoadMapScene()
        {
            await SceneLoader.LoadSceneAsync("Plain");

            UIManager.Instance.ClearCache();

            MapChanged?.Invoke();

            UIManager.Instance.ShowUI<GameView>();
            UIManager.Instance.ShowUI<OnScreenControllerView>();

            IsPlayerEnabled = true;
            ServiceCenter.Get<ICameraService>().EnableKineticCamera();
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

            player.transform.position = GameDataCenter.Instance.PlayerData.LastPosition;
            IsPlayerEnabled = false;
        }
    }
}