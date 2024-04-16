using KittyFarm.InventorySystem;
using UnityEngine;

namespace KittyFarm
{
    public abstract class ToolUsable : IUsableItem
    {
        protected readonly ItemDataSO data;
        protected readonly PlayerAnimation animation;
        protected readonly Vector2 direction;
        
        protected ToolUsable(ItemDataSO itemData, PlayerAnimation animation, Vector2 direction)
        {
            data = itemData;
            this.direction = direction;
            this.animation = animation;
        }

        public abstract void Use(Vector3 worldPosition, Vector3Int cellPosition);
    }
}