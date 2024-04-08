using System;
using System.Threading;
using System.Threading.Tasks;
using KittyFarm.CropSystem;
using KittyFarm.Map;
using KittyFarm.Time;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KittyFarm.UI
{
    public class GameView : UIBase
    {
        [SerializeField] private Button exitButton;
        [SerializeField] private ItemSlotGroup slotGroup;
        [SerializeField] private TextMeshProUGUI timeText;
        [SerializeField] private MapPropertiesBoard propertiesBoard;
        [SerializeField] private CropInfoBoard cropInfoBoard;

        public ItemSlot SelectedItem => slotGroup.SelectedSlot;

        private GridClickHandler gridClickHandler;

        private CancellationTokenSource propertiesBoardCTS;
        private CancellationTokenSource cropInfoBoardCTS;

        private void Awake()
        {
            gridClickHandler = FindObjectOfType<GridClickHandler>();
        }

        private void OnEnable()
        {
            TimeManager.Instance.MinutePassed += RefreshTimeBoard;
            gridClickHandler.TileClicked += ShowPropertiesBoard;
        }

        private void OnDisable()
        {
            TimeManager.Instance.MinutePassed -= RefreshTimeBoard;
            gridClickHandler.TileClicked -= ShowPropertiesBoard;
        }

        private void Start()
        {
            RefreshTimeBoard();

            exitButton.onClick.AddListener(Application.Quit);
        }

        private void RefreshTimeBoard()
        {
            var currentTime = TimeManager.Instance.CurrentTime;
            timeText.text = $"{currentTime.Hour} : {currentTime.Minute}";
        }

        private async void ShowPropertiesBoard(TilePropertiesInfo info)
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
    }
}