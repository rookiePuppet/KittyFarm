using System;

namespace KittyFarm.CropSystem
{
    public struct CropInfo
    {
        public string CropName;
        public int Stage;
        public float RipeRate;
        public TimeSpan GrowthTime;
        public CropStatus CropStatus;
        public string StatusName => CropStatus switch
        {
            CropStatus.Healthy => "健康",
            CropStatus.WaterLacked => "缺水",
            _ => "缺水"
        };
    }
}