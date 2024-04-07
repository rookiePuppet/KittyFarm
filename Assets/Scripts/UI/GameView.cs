using System;
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

        public ItemSlot SelectedItem => slotGroup.SelectedSlot;

        private GridClickHandler gridClickHandler;

        private void Awake()
        {
            gridClickHandler = FindObjectOfType<GridClickHandler>();
        }

        private void OnEnable()
        {
            TimeManager.Instance.MinutePassed += RefreshTimeBoard;
            gridClickHandler.TileClicked += RefreshPropertiesBoard;
        }

        private void OnDisable()
        {
            TimeManager.Instance.MinutePassed -= RefreshTimeBoard;
            gridClickHandler.TileClicked -= RefreshPropertiesBoard;
        }

        private void Start()
        {
            RefreshTimeBoard();

            exitButton.onClick.AddListener(Application.Quit);
            
            propertiesBoard.Hide();
        }

        private void RefreshTimeBoard()
        {
            var currentTime = TimeManager.Instance.CurrentTime;
            timeText.text = $"{currentTime.Hour} : {currentTime.Minute}";
        }

        private void RefreshPropertiesBoard(TilePropertiesInfo info)
        {
            propertiesBoard.Show();
            propertiesBoard.Refresh(info);
        }
    }
}