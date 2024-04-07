using KittyFarm.InventorySystem;
using KittyFarm.Service;
using UnityEngine;

namespace KittyFarm
{
    public class HoeUsable : IUsableItem
    {
        private readonly ItemDataSO data;

        private readonly PlayerAnimation animation;

        private readonly Vector2 direction;

        public HoeUsable(ItemDataSO itemData, PlayerAnimation animation, Vector2 direction)
        {
            data = itemData;
            this.direction = direction;
            this.animation = animation;
        }

        public void Use(Vector3 worldPosition, Vector3Int cellPosition)
        {
            var isDiggable = ServiceCenter.Get<IMapService>().IsPlantableAt(cellPosition);
            if (!isDiggable) return;
            
            var gridService = ServiceCenter.Get<IMapService>();
            var wasDug = gridService.CheckWasDugAt(cellPosition);
            if (wasDug) return;

            animation.PlayUseTool(direction, ToolType.Hoe, () => { gridService.DigAt(cellPosition); });
        }
    }
}