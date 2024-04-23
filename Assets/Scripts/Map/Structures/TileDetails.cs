using System;
using UnityEngine;

namespace KittyFarm.Map
{
    [Serializable]
    public class TileDetails
    {
        public Vector3Int CellPosition;
        
        public bool IsDug;
        
        public float WettingValue;
        public long LastWateringTimeTicks;
        public DateTime LastWateringTime
        {
            get => new(LastWateringTimeTicks);
            set => LastWateringTimeTicks = value.Ticks;
        }
    }
}