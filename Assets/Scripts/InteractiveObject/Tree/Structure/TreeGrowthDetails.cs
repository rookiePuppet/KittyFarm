using System;

namespace KittyFarm.InteractiveObject
{
    [Serializable]
    public class TreeGrowthDetails
    {
        public int CurrentStageIndex;
        public long PlantedTimeTicks;

        public DateTime PlantedTime
        {
            get => new(PlantedTimeTicks);
            set => PlantedTimeTicks = value.Ticks;
        }
    }
}