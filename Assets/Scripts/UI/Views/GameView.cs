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
        [SerializeField] private Button homeButton;
        [SerializeField] private ItemSlotGroup slotGroup;
        [SerializeField] private TextMeshProUGUI timeText;
        [SerializeField] private MapPropertiesBoard propertiesBoard;
        [SerializeField] private CropInfoBoard cropInfoBoard;
        [SerializeField] private TextMeshProUGUI coinsText;

        [SerializeField] private Button shopButton;
        
        public ItemSlot SelectedItem => slotGroup.SelectedSlot;
        
        private CancellationTokenSource propertiesBoardCTS;
        private CancellationTokenSource cropInfoBoardCTS;

        private void Awake()
        {
            homeButton.onClick.AddListener(OnHomeButtonClicked);
            shopButton.onClick.AddListener(OnShopButtonClicked);
        }

        private void OnEnable()
        {
            TimeManager.MinutePassed += RefreshTimeBoard;
            PlayerDataSO.CoinsUpdated += RefreshCoins;
        }

        private void OnDisable()
        {
            TimeManager.MinutePassed -= RefreshTimeBoard;
            PlayerDataSO.CoinsUpdated -= RefreshCoins;
        }

        private void Start()
        {
            RefreshTimeBoard();
            RefreshCoins(GameDataCenter.Instance.PlayerData.Coins);
        }

        private void OnShopButtonClicked()
        {
            UIManager.Instance.ShowUI<ShopWindow>();
        }

        private void OnHomeButtonClicked()
        {
            GameManager.BackToStartScene();
        }

        private void RefreshTimeBoard()
        {
            var currentTime = TimeManager.CurrentTime;
            timeText.text = currentTime.ToString("HH : mm");
        }

        public async void ShowPropertiesBoard(TilePropertiesInfo info)
        {
            propertiesBoardCTS?.Cancel();
            propertiesBoardCTS = new CancellationTokenSource();

            try
            {
                propertiesBoard.Show();
                propertiesBoard.Refresh(info);

                await Task.Delay(1500, propertiesBoardCTS.Token);
                propertiesBoard.Hide();
            }
            catch (OperationCanceledException)
            {
            }
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

        private void RefreshCoins(int coins)
        {
            coinsText.text = coins.ToString();
        }
    }
}