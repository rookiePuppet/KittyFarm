using KittyFarm.InventorySystem;
using KittyFarm.Service;
using UnityEngine;

namespace KittyFarm
{
    public class HoeUsable : ToolUsable
    {
        public HoeUsable(ItemDataSO itemData, PlayerAnimation animation, Vector2 direction) : base(itemData, animation,
            direction)
        {
        }

        public override async void Use(Vector3 worldPosition, Vector3Int cellPosition)
        {
            if (animation.IsUsingTool) return;
            
            var isDiggable = ServiceCenter.Get<ITilemapService>().IsPlantableAt(cellPosition);
            if (!isDiggable) return;

            var tilemapService = ServiceCenter.Get<ITilemapService>();
            var wasDug = tilemapService.CheckWasDugAt(cellPosition);
            if (wasDug) return;
            
            InputReader.DisableInput();
            
            await animation.PlayUseTool(direction, ToolType.Hoe);
            
            tilemapService.DigAt(cellPosition);
            InputReader.EnableInput();
            
        }
    }
}