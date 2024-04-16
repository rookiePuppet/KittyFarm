using System;
using KittyFarm.InventorySystem;
using KittyFarm.Service;
using KittyFarm.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace KittyFarm.Map
{
    public class GridClickHandler : MonoBehaviour, IPointerClickHandler
    {
        private ItemSlot SelectedItem => UIManager.Instance.GetUI<GameView>().SelectedItem;
        private ItemDataSO SelectedItemData => SelectedItem.ItemData;

        private PlayerController player;
        private GridCellIndicator indicator;

        private Grid grid;

        public event Action<TilePropertiesInfo> TileClicked;

        private void Awake()
        {
            player = FindObjectOfType<PlayerController>();
            indicator = FindObjectOfType<GridCellIndicator>();

            grid = GetComponent<Grid>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            HandleGridClick(eventData.position);
        }
        
        private void HandleGridClick(Vector2 screenPosition)
        {
            var worldPosition = ServiceCenter.Get<IPointerService>().ScreenToWorldPoint(screenPosition);

            var cellPosition = grid.WorldToCell(worldPosition);
            var cellCenter = grid.GetCellCenterWorld(cellPosition);

            if (indicator != null) indicator.ShowAt(cellCenter);

            TileCheck(cellPosition);

            if (SelectedItem != null)
            {
                try
                {
                    var actionDirection = cellCenter - transform.position;

                    IUsableItem usableItem = SelectedItemData.Type switch
                    {
                        ItemType.Seed => new SeedUsable(SelectedItemData as SeedDataSO),
                        ItemType.Hoe => new HoeUsable(SelectedItemData, player.Animation, actionDirection),
                        ItemType.FarmProduct => new FarmProductUsable(SelectedItemData),
                    };
                    usableItem.Use(worldPosition, cellPosition);
                }
                catch (ArgumentOutOfRangeException)
                {
                }
            }
        }

        private void TileCheck(Vector3Int cellPosition)
        {
            var info = ServiceCenter.Get<ITilemapService>().GetTilePropertiesInfoAt(cellPosition);
            UIManager.Instance.GetUI<GameView>().ShowPropertiesBoard(info);
        }
    }
}