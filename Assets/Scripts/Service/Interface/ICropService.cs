using KittyFarm.Data;
using KittyFarm.InteractiveObject;
using UnityEngine;

namespace KittyFarm.Service
{
    public interface ICropService : IService
    {
        public CropDatabaseSO CropDatabase { get; }
        public CropGrowthTracker GrowthTracker { get; }
        public bool IsCropRipe(CropGrowthDetails growthDetails);
        public int HarvestCrop(Crop crop);
        public void PlantCrop(CropDataSO cropData, Vector3Int cellPosition);
        public bool IsCropExistentAt(Vector3Int cellPosition);
        public bool TryGetCropAt(Vector3Int cellPosition, out Crop crop);
        public bool IsCropRipeAt(Vector3Int cellPosition);
    }
}