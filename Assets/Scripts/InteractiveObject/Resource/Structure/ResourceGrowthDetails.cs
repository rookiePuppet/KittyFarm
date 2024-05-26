using System;
using UnityEngine;

namespace KittyFarm.InteractiveObject
{
    [Serializable]
    public class ResourceGrowthDetails
    {
        public int CurrentStageIndex;
        public long LastCollectTimeTicks;
        public DateTime LastCollectTime
        {
            get => new(LastCollectTimeTicks);
            set => LastCollectTimeTicks = value.Ticks;
        }
    }
}