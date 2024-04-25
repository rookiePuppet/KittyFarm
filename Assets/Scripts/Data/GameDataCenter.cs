using Framework;
using FrameWork;
using KittyFarm.CropSystem;
using KittyFarm.InventorySystem;
using KittyFarm.Map;
using UnityEngine;

namespace KittyFarm.Data
{
    public class GameDataCenter : MonoSingleton<GameDataCenter>
    {
        [SerializeField] private MapDataSO mapData;
        public MapDataSO MapData => mapData;
        
        public PlayerInventorySO PlayerInventory => playerInventory;
        public MapCropsDataSO MapCropsData => mapCropsData;
        public MapTilesDataSO MapTilesData => mapTilesData;
        public MapResourcesDataSO MapResourcesData => mapResourcesData;

        private PlayerInventorySO playerInventory;
        private MapCropsDataSO mapCropsData;
        private MapTilesDataSO mapTilesData;
        private MapResourcesDataSO mapResourcesData;

        private void OnEnable()
        {
            GameManager.BeforeGameExit += OnBeforeGameExit;
        }

        private void OnDisable()
        {
            GameManager.BeforeGameExit -= OnBeforeGameExit;
        }

        private void Start()
        {
            LoadPlayerInventory();
            JsonDataManager.LoadData(MapCropsDataSO.PersistentDataName, out mapCropsData);
            JsonDataManager.LoadData(MapTilesDataSO.PersistentDataName, out mapTilesData);
            JsonDataManager.LoadData(MapResourcesDataSO.PersistentDataName, out mapResourcesData);
        }

        private void LoadPlayerInventory()
        {
            JsonDataManager.LoadData(PlayerInventorySO.PersistentDataName, out playerInventory);
            playerInventory.Initialize();
        }

        private void OnBeforeGameExit()
        {
            JsonDataManager.SaveData(PlayerInventorySO.PersistentDataName, playerInventory);
            JsonDataManager.SaveData(MapCropsDataSO.PersistentDataName, mapCropsData);
            JsonDataManager.SaveData(MapTilesDataSO.PersistentDataName, mapTilesData);
            JsonDataManager.SaveData(MapResourcesDataSO.PersistentDataName, mapResourcesData);
        }

        public void FirstCreateMapResourcesData(MapResourcesDataSO data)
        {
            mapResourcesData = data;
            JsonDataManager.SaveData(MapResourcesDataSO.PersistentDataName, mapResourcesData);
        }
    }
}