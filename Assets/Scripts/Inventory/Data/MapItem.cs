using System;
using UnityEngine;

namespace KittyFarm.InventorySystem
{
    [Serializable]
    public class MapItem
    {
        public Vector3 Position;
        public int ItemId;
        public int Count;

        public MapItem(Vector3 position, int itemId, int count)
        {
            Position = position;
            ItemId = itemId;
            Count = count;
        }
    }
}