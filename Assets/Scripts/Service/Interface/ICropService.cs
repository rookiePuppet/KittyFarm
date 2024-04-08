using KittyFarm.CropSystem;
using UnityEngine;

namespace KittyFarm.Service
{
    public interface ICropService : IService
    {
        public int HarvestCrop(Crop crop);
        public void PlantCrop(CropDataSO cropData, Vector3Int cellPosition);
        public bool CheckCropExistsAt(Vector3Int cellPosition);
        public void LoadCropsOnMap(MapCropsDataSO data);
        public bool CheckCropIsRipeAt(Vector3Int cellPosition);
    }
}