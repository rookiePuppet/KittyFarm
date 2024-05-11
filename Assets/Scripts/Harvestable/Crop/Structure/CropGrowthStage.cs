using System;
using UnityEngine;

namespace KittyFarm.Harvestable
{
    [Serializable]
    public class CropGrowthStage
    {
        public int GrowthHours;
        public int GrowthMinutes;
        public Sprite Sprite;
    }
}