using System;
using UnityEngine;

namespace KittyFarm.CropSystem
{
    [Serializable]
    public class CropGrowthDetails
    {
        public Vector3Int CellPosition;
        public CropDataSO Data;
        public int CurrentStage = 0;
        public DateTime PlantedTime;

        public int MaxStage => Data.Stages.Length - 1;
        public bool IsRipe => CurrentStage >= MaxStage;

        public CropGrowthDetails(Vector3Int cellPosition, CropDataSO data, int initialStage = 0)
        {
            CellPosition = cellPosition;
            Data = data;
            CurrentStage = initialStage;
            PlantedTime = DateTime.Now;
        }
    }
}