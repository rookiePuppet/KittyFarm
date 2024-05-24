using System;

namespace KittyFarm.InteractiveObject
{
    public struct CropInfo
    {
        public string CropName;
        public int Stage;
        public float RipeRate;
        public TimeSpan GrowthTime;
    }
}