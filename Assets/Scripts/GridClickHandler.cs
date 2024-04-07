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
        private ItemDataSO SelectedItemData => SelectedItem.Item.itemData;

        private PlayerController player;
        private GridCellIndicator indicator;

        public event Action<TilePropertiesInfo> TileClicked;

        private void Awake()
        {
            player = FindObjectOfType<PlayerController>();
            indicator = FindObjectOfType<GridCellIndicator>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            print(eventData.position);
            HandleGridClick(eventData.position);
        }
        
        private void HandleGridClick(Vector2 screenPosition)
        {
            var worldPosition = ServiceCenter.Get<IPointerService>().ScreenToWorldPoint(screenPosition);

            var mapService = ServiceCenter.Get<IMapService>();
            var cellPosition = mapService.WorldToCell(worldPosition);
            var cellCenter = mapService.GetCellCenterWorld(cellPosition);

            print("Click on grid");

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
            var info = ServiceCenter.Get<IMapService>().GetTilePropertiesInfoAt(cellPosition);
            TileClicked?.Invoke(info);
        }
    }
}