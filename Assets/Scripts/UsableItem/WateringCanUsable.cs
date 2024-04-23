using KittyFarm.InventorySystem;
using KittyFarm.Service;
using UnityEngine;

namespace KittyFarm
{
    public class WateringCanUsable: ToolUsable
    {
        public WateringCanUsable(Vector3 worldPosition, Vector3Int cellPosition, ItemDataSO itemData, PlayerAnimation animation, Vector2 direction, bool existsCrop) : base(worldPosition, cellPosition, itemData, animation, direction)
        {
            CanUse = existsCrop && !animation.IsUsingTool;
        }

        public override async void Use()
        {
            var tilemapService = ServiceCenter.Get<ITilemapService>();
            
            Debug.Log("使用浇水壶");
            
            InputReader.DisableInput();
            
            await animation.PlayUseTool(direction, ToolType.WateringCan);
            
            tilemapService.WaterAt(cellPosition);
            
            Debug.Log("浇水完成");
            InputReader.EnableInput();
        }
    }
}