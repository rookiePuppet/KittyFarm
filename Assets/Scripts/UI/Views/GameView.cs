using System;
using System.Threading;
using System.Threading.Tasks;
using KittyFarm.CropSystem;
using KittyFarm.Data;
using KittyFarm.Time;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KittyFarm.UI
{
    public class GameView : UIBase
    {
        [SerializeField] private ItemSlotGroup slotGroup;
        [SerializeField] private TextMeshProUGUI timeText;
        // [SerializeField] private MapPropertiesBoard propertiesBoard;
        [SerializeField] private CropInfoBoard cropInfoBoard;
        [SerializeField] private TextMeshProUGUI coinsText;
        [SerializeField] private Button homeButton;
        [SerializeField] private Button shopButton;
        [SerializeField] private Button settingsButton;
        
        public ItemSlot SelectedItem => slotGroup.SelectedSlot;

        // private CancellationTokenSource propertiesBoardCTS;
        private CancellationTokenSource cropInfoBoardCTS;

        private void OnEnable()
        {
            homeButton.onClick.AddListener(OnHomeButtonClicked);
            shopButton.onClick.AddListener(OnShopButtonClicked);
            settingsButton.onClick.AddListener(OnSettingsButtonClicked);

            TimeManager.MinutePassed += RefreshTimeBoard;
            PlayerDataSO.CoinsUpdated += RefreshCoins;
            slotGroup.OnItemSelected += OnItemSelected;
        }
        
        private void OnDisable()
        {
            homeButton.onClick.RemoveListener(OnHomeButtonClicked);
            shopButton.onClick.RemoveListener(OnShopButtonClicked);
            settingsButton.onClick.RemoveListener(OnSettingsButtonClicked);

            TimeManager.MinutePassed -= RefreshTimeBoard;
            PlayerDataSO.CoinsUpdated -= RefreshCoins;
            slotGroup.OnItemSelected -= OnItemSelected;
        }

        private void Start()
        {
            RefreshTimeBoard();
            RefreshCoins(GameDataCenter.Instance.PlayerData.Coins);
        }
        
        public async void ShowCropInfoBoard(CropInfo cropInfo)
        {
            cropInfoBoardCTS?.Cancel();
            cropInfoBoardCTS = new CancellationTokenSource();

            try
            {
                cropInfoBoard.Show();
                cropInfoBoard.Refresh(cropInfo);

                await Task.Delay(1500, cropInfoBoardCTS.Token);
                cropInfoBoard.Hide();
            }
            catch (OperationCanceledException)
            {
            }
        }
        
        private void RefreshTimeBoard()
        {
            var currentTime = TimeManager.CurrentTime;
            timeText.text = currentTime.ToString("HH : mm");
        }

        private void RefreshCoins(int coins)
        {
            coinsText.text = coins.ToString();
        }

        private void OnShopButtonClicked()
        {
            UIManager.Instance.ShowUI<ShopWindow>();
        }

        private void OnHomeButtonClicked()
        {
            GameManager.BackToStartScene();
        }

        private void OnSettingsButtonClicked()
        {
            UIManager.Instance.ShowUI<SettingsWindow>();
        }
        
        private void OnItemSelected(ItemDataSO itemData)
        {
            GameManager.Player.SpeakBubble.Show($"这是{itemData.ItemName}");
        }
        
        // public async void ShowPropertiesBoard(TilePropertiesInfo info)
        // {
        //     propertiesBoardCTS?.Cancel();
        //     propertiesBoardCTS = new CancellationTokenSource();
        //
        //     try
        //     {
        //         propertiesBoard.Show();
        //         propertiesBoard.Refresh(info);
        //
        //         await Task.Delay(1500, propertiesBoardCTS.Token);
        //         propertiesBoard.Hide();
        //     }
        //     catch (OperationCanceledException)
        //     {
        //     }
        // }
    }
}