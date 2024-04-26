using KittyFarm.Data;
using KittyFarm.InventorySystem;
using UnityEngine;

namespace KittyFarm
{
    public abstract class ToolUsable : IUsableItem
    {
        public bool CanUse { get; protected set; }
        
        protected readonly ItemDataSO data;
        protected readonly PlayerAnimation animation;
        protected readonly Vector2 direction;       
        protected readonly Vector3 worldPosition;
        protected readonly Vector3Int cellPosition;

        protected ToolUsable(Vector3 worldPosition, Vector3Int cellPosition, ItemDataSO itemData,
            PlayerAnimation animation, Vector2 direction)
        {
            this.worldPosition = worldPosition;
            this.cellPosition = cellPosition;
            this.direction = direction;
            this.animation = animation;
            data = itemData;
        }

        public abstract void Use();
    }
}