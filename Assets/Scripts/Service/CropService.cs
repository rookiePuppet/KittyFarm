using KittyFarm.CropSystem;
using UnityEngine;

namespace KittyFarm.Service
{
    public class CropService : MonoBehaviour, ICropService
    {
        [SerializeField] private GameObject cropPrefab;
        [SerializeField] private Vector3 cropOnGridCellOffset = new(0.5f, 0.25f);
        
        private CropGrowthTracker growthTracker;

        private MapCropsDataSO cropsData;

        public void LoadCropsOnMap(MapCropsDataSO data)
        {
            cropsData = data;
            
            growthTracker = new GameObject("Crops").AddComponent<CropGrowthTracker>();

            foreach (var growthDetails in cropsData.GrowthDetails)
            {
                var crop = SpawnCrop(growthDetails.CellPosition);
                crop.Initialize(growthDetails);
            }
        }

        public void PlantCrop(CropDataSO cropData, Vector3Int cellPosition)
        {
            var crop = SpawnCrop(cellPosition);
            var growthDetails = new CropGrowthDetails(cellPosition, cropData);
            crop.Initialize(growthDetails);
            
            cropsData.SaveCropData(growthDetails);

            Debug.Log("Crop Planted: " + growthDetails.PlantedTime);
        }

        public bool CheckCropIsRipeAt(Vector3Int cellPosition)
        {
            if (TryGetCropAt(cellPosition, out var crop))
            {
                return crop.GrowthDetails.IsRipe;
            }

            return false;
        }

        private Crop SpawnCrop(Vector3Int cellPosition)
        {
            var cropObj = Instantiate(cropPrefab, growthTracker.transform);
            cropObj.transform.position = cellPosition + cropOnGridCellOffset;

            var crop = cropObj.GetComponent<Crop>();
            growthTracker.AddCrop(crop);
            
            return crop;
        }

        public bool CheckCropExistsAt(Vector3Int cellPosition)
        {
            var cellCenter = ServiceCenter.Get<IMapService>().GetCellCenterWorld(cellPosition);
            var result = Physics2D.OverlapCircle(cellCenter, 0.5f, LayerMask.GetMask("Crop"));
            return result != null;
        }

        private bool TryGetCropAt(Vector3Int cellPosition, out Crop crop)
        {
            var cellCenter = ServiceCenter.Get<IMapService>().GetCellCenterWorld(cellPosition);
            var overlapResult = Physics2D.OverlapPoint(cellCenter, LayerMask.GetMask("Crop"));
            if (overlapResult == null)
            {
                crop = null;
                return false;
            }

            crop = overlapResult.GetComponent<Crop>();
            return true;
        }
    }
}