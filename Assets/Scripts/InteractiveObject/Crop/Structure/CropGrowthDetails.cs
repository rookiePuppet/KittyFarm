using System;
using KittyFarm.Time;
using UnityEngine;

namespace KittyFarm.InteractiveObject
{
    [Serializable]
    public class CropGrowthDetails
    {
        public Vector3Int CellPosition;
        public int CropId;
        public int CurrentStage;
        public long PlantedTimeTicks;
        public DateTime PlantedTime => new(PlantedTimeTicks);

        public CropGrowthDetails(Vector3Int cellPosition, int cropId, int initialStage = 0)
        {
            CellPosition = cellPosition;
            CropId = cropId;
            CurrentStage = initialStage;
            PlantedTimeTicks = TimeManager.CurrentTime.Ticks;
        }
    }
}