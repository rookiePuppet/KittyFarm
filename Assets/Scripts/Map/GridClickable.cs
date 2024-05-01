using System;
using System.Linq;
using KittyFarm.Data;
using KittyFarm.Service;
using KittyFarm.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace KittyFarm.Map
{
    public class GridClickable : MonoBehaviour, IPointerClickHandler
    {
        public static Action<Vector3Int, Vector3> TileClicked;

        private ItemSlot SelectedItem => UIManager.Instance.GetUI<GameView>().SelectedItem;
        private ItemDataSO SelectedItemData => SelectedItem.ItemData;

        private ITilemapService tilemapService;
        private ICameraService cameraService;
        private IItemService itemService;

        private void Awake()
        {
            tilemapService = ServiceCenter.Get<ITilemapService>();
            cameraService = ServiceCenter.Get<ICameraService>();
            itemService = ServiceCenter.Get<IItemService>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            var worldPosition = cameraService.ScreenToWorldPoint(eventData.position);
            var cellPosition = tilemapService.WorldToCell(worldPosition);
            var cellCenter = tilemapService.GetCellCenterWorld(cellPosition);

            HandleItemUsing(worldPosition, cellPosition);

            var info = tilemapService.GetTilePropertiesInfoAt(cellPosition);
            UIManager.Instance.GetUI<GameView>().ShowPropertiesBoard(info);

            TileClicked?.Invoke(cellPosition, cellCenter);
        }

        private void HandleItemUsing(Vector3 worldPosition, Vector3Int cellPosition)
        {
            if (SelectedItem == null) return;

            var usableItem = itemService.TakeUsableItem(SelectedItemData, worldPosition, cellPosition);
            var judgements = usableItem.JudgeUsable().ToArray();

            if (judgements.Length > 0)
            {
                Debug.LogWarning(judgements[0]);
                UIManager.Instance.ShowMessage(judgements[0]);
                return;
            }

            usableItem.Use();
        }
    }
}