using KittyFarm.CropSystem;
using KittyFarm.Data;
using KittyFarm.InventorySystem;
using KittyFarm.Time;
using UnityEngine;

namespace KittyFarm.Service
{
    public class CropService : MonoBehaviour, ICropService
    {
        [SerializeField] private GameObject cropPrefab;
        [SerializeField] private Vector3 cropOnGridCellOffset = new(0.5f, 0.25f);
        [SerializeField] private CropDatabaseSO cropDatabase;

        public CropDatabaseSO CropDatabase => cropDatabase;
        private PlayerInventorySO PlayerInventory => GameDataCenter.Instance.PlayerInventory;
        private MapCropsDataSO cropsData;
        
        private CropGrowthTracker growthTracker;

        private void OnEnable()
        {
            SceneLoader.MapLoaded += Initialize;
        }

        private void OnDisable()
        {
            SceneLoader.MapLoaded -= Initialize;
        }

        private void Initialize(int mapId)
        {
            cropsData = GameDataCenter.Instance.GetMapCropsData(mapId);
            
            growthTracker = new GameObject("Crops").AddComponent<CropGrowthTracker>();

            foreach (var growthDetails in cropsData.GrowthDetails)
            {
                var crop = SpawnCrop(growthDetails.CellPosition);
                crop.Initialize(growthDetails);
            }
        }

        public int HarvestCrop(Crop crop)
        {
            var growthDetails = crop.GrowthDetails;

            var cropData = cropDatabase.GetCropData(growthDetails.DataId);

            // 添加
            PlayerInventory.AddItem(cropData.ProductData, 1);

            // 删除地图上该位置作物数据
            cropsData.RemoveCrop(growthDetails);

            growthTracker.RemoveCrop(crop);

            // TODO：随机数量机制
            // 返回收获数量
            return 1;
        }

        public void PlantCrop(CropDataSO cropData, Vector3Int cellPosition)
        {
            var crop = SpawnCrop(cellPosition);
            var growthDetails = new CropGrowthDetails(cellPosition, cropData.Id);
            crop.Initialize(growthDetails);

            cropsData.AddCrop(growthDetails);
        }

        public bool CheckCropIsRipeAt(Vector3Int cellPosition)
        {
            if (!TryGetCropAt(cellPosition, out var crop)) return false;

            var cropData = cropDatabase.GetCropData(crop.GrowthDetails.DataId);
            var growthMinutes = (TimeManager.Instance.CurrentTime - crop.GrowthDetails.PlantedTime).TotalMinutes;

            return growthMinutes > cropData.TotalMinutesToBeRipe;
        }

        public bool IsCropRipe(CropGrowthDetails growthDetails)
        {
            var cropData = cropDatabase.GetCropData(growthDetails.DataId);
            var growthMinutes = (TimeManager.Instance.CurrentTime - growthDetails.PlantedTime).TotalMinutes;

            return growthMinutes > cropData.TotalMinutesToBeRipe;
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
            var cellCenter = ServiceCenter.Get<ITilemapService>().GetCellCenterWorld(cellPosition);
            var result = Physics2D.OverlapCircle(cellCenter, 0.5f, LayerMask.GetMask("Crop"));
            return result != null;
        }

        private bool TryGetCropAt(Vector3Int cellPosition, out Crop crop)
        {
            var cellCenter = ServiceCenter.Get<ITilemapService>().GetCellCenterWorld(cellPosition);
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