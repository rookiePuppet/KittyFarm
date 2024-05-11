using System;
using UnityEngine;

namespace KittyFarm.Harvestable
{
    [Serializable]
    public class ResourceGrowthDetails
    {
        public Vector3 Position;
        public int ResourceId;
        public long LastCollectTimeTicks;
        public DateTime LastCollectTime
        {
            get => new(LastCollectTimeTicks);
            set => LastCollectTimeTicks = value.Ticks;
        }
    }
}