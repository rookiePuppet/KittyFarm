using System;
using UnityEngine;

namespace KittyFarm.CropSystem
{
    [Serializable]
    public class CropGrowthStage
    {
        public int GrowthDuration; // 该阶段的生长时间（小时）
        public Sprite Sprite; // 该阶段的图片
    }
}