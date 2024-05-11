using System;

namespace KittyFarm.Harvestable
{
    public struct CropInfo
    {
        public string CropName;
        public int Stage;
        public float RipeRate;
        public TimeSpan GrowthTime;
    }
}