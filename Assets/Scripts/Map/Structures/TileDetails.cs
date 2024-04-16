using System;
using UnityEngine;

namespace KittyFarm.Map
{
    [Serializable]
    public class TileDetails
    {
        public Vector3Int CellPosition;
        public bool IsDug;
        public bool IsWatered;
    }
}