using System;
using UnityEngine;

namespace KittyFarm.CropSystem
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