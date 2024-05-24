using System;
using UnityEngine;

namespace KittyFarm.InteractiveObject
{
    [Serializable]
    public class ResourceGrowthDetails
    {
        public Vector3 Position;
        public int ResourceId;
        public int CurrentStage;
        
        public long LastCollectTimeTicks;
        public DateTime LastCollectTime
        {
            get => new(LastCollectTimeTicks);
            set => LastCollectTimeTicks = value.Ticks;
        }
    }
}