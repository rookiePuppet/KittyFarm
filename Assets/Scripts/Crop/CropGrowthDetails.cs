using System;
using UnityEngine;

namespace KittyFarm.CropSystem
{
    [Serializable]
    public class CropGrowthDetails
    {
        public Vector3Int CellPosition;
        public int CropId;
        public int CurrentStage;
        public long PlantedTimeTicks;
        //public CropStatus Status;
        public DateTime PlantedTime => new(PlantedTimeTicks);

        public CropGrowthDetails(Vector3Int cellPosition, int cropId, int initialStage = 0)
        {
            CellPosition = cellPosition;
            CropId = cropId;
            CurrentStage = initialStage;
            PlantedTimeTicks = DateTime.Now.Ticks;
            // PlantedTimeTicks = (DateTime.Now - TimeSpan.FromMinutes(59)-TimeSpan.FromSeconds(55)).Ticks;

            // Status = CropStatus.Healthy;
        }
    }

    public enum CropStatus
    {
        Healthy,
        WaterLacked
    }
}