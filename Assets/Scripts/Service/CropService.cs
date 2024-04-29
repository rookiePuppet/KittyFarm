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
        public CropGrowthTracker GrowthTracker => growthTracker;
        private PlayerInventorySO PlayerInventory => GameDataCenter.Instance.PlayerInventory;
        private MapCropsDataSO cropsData;

        private CropGrowthTracker growthTracker;

        private void OnEnable()
        {
            GameManager.MapChanged += Initialize;
        }

        private void OnDisable()
        {
            GameManager.MapChanged -= Initialize;
        }

        private void Initialize()
        {
            cropsData = GameDataCenter.Instance.MapCropsData;

            growthTracker = new GameObject("Crops").AddComponent<CropGrowthTracker>();

            foreach (var growthDetails in cropsData.CropDetailsList)
            {
                var crop = SpawnCrop(growthDetails.CellPosition);
                crop.Initialize(growthDetails);
            }
        }

        public int HarvestCrop(Crop crop)
        {
            var growthDetails = crop.GrowthDetails;

            var cropData = cropDatabase.GetCropData(growthDetails.CropId);

            // 添加
            PlayerInventory.AddItem(cropData.ProductData, 1);

            // 删除地图上该位置作物数据
            cropsData.RemoveCrop(growthDetails);

            growthTracker.RemoveCrop(crop);

            print($"收获了1个{cropData.CropName}");
            Destroy(crop.gameObject);

            // TODO：随机数量机制

            return 1;
        }

        public void PlantCrop(CropDataSO cropData, Vector3Int cellPosition)
        {
            var crop = SpawnCrop(cellPosition);
            var growthDetails = new CropGrowthDetails(cellPosition, cropData.Id);
            crop.Initialize(growthDetails);

            cropsData.AddCrop(growthDetails);
        }

        public bool IsCropRipeAt(Vector3Int cellPosition) =>
            TryGetCropAt(cellPosition, out var crop) && IsCropRipe(crop.GrowthDetails);

        public bool IsCropRipe(CropGrowthDetails growthDetails)
        {
            var cropData = cropDatabase.GetCropData(growthDetails.CropId);
            var growthMinutes = TimeManager.GetTimeSpanFrom(growthDetails.PlantedTime).TotalMinutes;

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

        public bool IsCropExistentAt(Vector3Int cellPosition)
        {
            var cellCenter = ServiceCenter.Get<ITilemapService>().GetCellCenterWorld(cellPosition);
            var result = Physics2D.OverlapCircle(cellCenter, 0.5f, LayerMask.GetMask("Crop"));
            return result != null;
        }

        public bool TryGetCropAt(Vector3Int cellPosition, out Crop crop)
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