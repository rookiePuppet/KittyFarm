using System;
using UnityEngine;

namespace KittyFarm.CropSystem
{
    [Serializable]
    public class CropGrowthDetails
    {
        public Vector3Int CellPosition;
        public int DataId;
        public int CurrentStage;
        public DateTime PlantedTime;

        public CropGrowthDetails(Vector3Int cellPosition, int dataId, int initialStage = 0)
        {
            CellPosition = cellPosition;
            DataId = dataId;
            CurrentStage = initialStage;
            PlantedTime = DateTime.Now;
        }
    }
}