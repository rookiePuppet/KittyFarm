using KittyFarm.Data;
using KittyFarm.InventorySystem;
using KittyFarm.Service;
using UnityEngine;

namespace KittyFarm
{
    public class HoeUsable : ToolUsable
    {
        private readonly ITilemapService tilemapService;

        public HoeUsable(Vector3 worldPosition, Vector3Int cellPosition, ItemDataSO itemData, PlayerAnimation animation,
            Vector2 direction) : base(worldPosition, cellPosition, itemData, animation, direction)
        {
            tilemapService = ServiceCenter.Get<ITilemapService>();

            var isPlantable = tilemapService.IsPlantableAt(cellPosition);
            var wasDug = tilemapService.CheckWasDugAt(cellPosition);

            CanUse = isPlantable && !wasDug && !animation.IsUsingTool;
        }

        public override async void Use()
        {
            if (!CanUse) return;

            InputReader.DisableInput();
            await animation.PlayUseTool(direction, ToolType.Hoe, () => { tilemapService.DigAt(cellPosition); });
            InputReader.EnableInput();
        }
    }
}