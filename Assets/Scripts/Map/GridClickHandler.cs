using System;
using Framework;
using KittyFarm.InventorySystem;
using KittyFarm.Service;
using KittyFarm.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace KittyFarm.Map
{
    public class GridClickHandler : MonoSingleton<GridClickHandler>
    {
        private ItemSlot SelectedItem => UIManager.Instance.GetUI<GameView>().SelectedItem;
        private ItemDataSO SelectedItemData => SelectedItem.ItemData;

        private PlayerController player;
        private GridCellIndicator indicator;

        private Vector3 clickPoint;

        private bool existsCropAtClickPoint;

        private ITilemapService tilemapService;
        private IPointerService pointerService;

        private bool isMapLoaded;
        private bool isPointOnUI;

        public event Action<TilePropertiesInfo> TileClicked;

        private void OnEnable()
        {
            SceneLoader.MapLoaded += OnMapLoaded;
            InputReader.Click += OnClick;
        }

        private void OnDisable()
        {
            SceneLoader.MapLoaded -= OnMapLoaded;
            InputReader.Click -= OnClick;
        }

        private void OnMapLoaded(int mapId)
        {
            player = FindObjectOfType<PlayerController>();
            indicator = FindObjectOfType<GridCellIndicator>();

            isMapLoaded = true;
        }

        private void Start()
        {
            tilemapService = ServiceCenter.Get<ITilemapService>();
            pointerService = ServiceCenter.Get<IPointerService>();
        }

        private void Update()
        {
            isPointOnUI = EventSystem.current.IsPointerOverGameObject();
        }

        private void OnClick(InputAction.CallbackContext context)
        {
            if (!isMapLoaded || isPointOnUI || !context.started) return;

            clickPoint = InputReader.Point;
            var worldPosition = pointerService.ScreenToWorldPoint(clickPoint);
            worldPosition.z = 0;
            var cellPosition = tilemapService.WorldToCell(worldPosition);
            var cellCenter = tilemapService.GetCellCenterWorld(cellPosition);

            var hitOnGrid = Physics2D.Raycast(worldPosition, Vector3.forward, 0.1f, LayerMask.GetMask("Map"));
            if (!hitOnGrid)
            {
                return;
            }

            if (indicator != null)
            {
                indicator.ShowAt(cellCenter);
            }

            ShowTileProperties(cellPosition);
            CheckCrop(cellPosition);

            if (SelectedItem != null)
            {
                var actionDirection = (cellCenter - transform.position).normalized;

                IUsableItem usableItem = SelectedItemData.Type switch
                {
                    ItemType.Seed => new SeedUsable(worldPosition, cellPosition, SelectedItemData as SeedDataSO),
                    ItemType.Hoe => new HoeUsable(worldPosition, cellPosition, SelectedItemData, player.Animation,
                        actionDirection),
                    ItemType.FarmProduct => new FarmProductUsable(worldPosition, cellPosition, SelectedItemData),
                    ItemType.WateringCan => new WateringCanUsable(worldPosition, cellPosition, SelectedItemData,
                        player.Animation, actionDirection, existsCropAtClickPoint),
                };
                usableItem.Use();
            }
        }

        private void CheckCrop(Vector3Int cellPosition)
        {
            var cropService = ServiceCenter.Get<ICropService>();

            existsCropAtClickPoint = cropService.TryGetCropAt(cellPosition, out var crop);

            if (existsCropAtClickPoint)
            {
                UIManager.Instance.GetUI<GameView>().ShowCropInfoBoard(crop.Info);
            }
        }

        private void ShowTileProperties(Vector3Int cellPosition)
        {
            var info = tilemapService.GetTilePropertiesInfoAt(cellPosition);
            UIManager.Instance.GetUI<GameView>().ShowPropertiesBoard(info);
        }
    }
}