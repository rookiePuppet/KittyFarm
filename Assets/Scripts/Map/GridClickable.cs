using System;
using KittyFarm.Data;
using KittyFarm.InventorySystem;
using KittyFarm.Service;
using KittyFarm.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace KittyFarm.Map
{
    public class GridClickable : MonoBehaviour, IPointerClickHandler
    {
        private ItemSlot SelectedItem => UIManager.Instance.GetUI<GameView>().SelectedItem;
        private ItemDataSO SelectedItemData => SelectedItem.ItemData;

        private PlayerController player;

        private ITilemapService tilemapService;
        private IPointerService pointerService;

        public static Action<Vector3Int, Vector3> TileClicked;
        
        private void Start()
        {
            player = FindObjectOfType<PlayerController>();
            
            tilemapService = ServiceCenter.Get<ITilemapService>();
            pointerService = ServiceCenter.Get<IPointerService>();
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            var worldPosition = pointerService.ScreenToWorldPoint(eventData.position);
            var cellPosition = tilemapService.WorldToCell(worldPosition);
            var cellCenter = tilemapService.GetCellCenterWorld(cellPosition);
            
            if (SelectedItem != null)
            {
                var actionDirection = (cellCenter - player.transform.position).normalized;
                IUsableItem usableItem = SelectedItemData.Type switch
                {
                    ItemType.Seed => new SeedUsable(worldPosition, cellPosition, SelectedItemData as SeedDataSO),
                    ItemType.Hoe => new HoeUsable(worldPosition, cellPosition, SelectedItemData, player.Animation,
                        actionDirection),
                    ItemType.FarmProduct => new FarmProductUsable(worldPosition, cellPosition, SelectedItemData),
                    _ => new FarmProductUsable(worldPosition, cellPosition, SelectedItemData)
                };
                usableItem.Use();
            }
            
            var info = tilemapService.GetTilePropertiesInfoAt(cellPosition);
            UIManager.Instance.GetUI<GameView>().ShowPropertiesBoard(info);
            
            TileClicked?.Invoke(cellPosition, cellCenter);
        }
    }
}