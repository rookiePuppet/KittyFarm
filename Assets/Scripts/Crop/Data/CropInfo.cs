using System;

namespace KittyFarm.CropSystem
{
    public struct CropInfo
    {
        public string CropName;
        public int Stage;
        public float RipeRate;
        public TimeSpan GrowthTime;
    }
}