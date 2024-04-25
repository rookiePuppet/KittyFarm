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

        public PlayerInventorySO PlayerInventory
        {
            get
            {
                if (playerInventory == null)
                {
                    JsonDataManager.LoadData(PlayerInventorySO.PersistentDataName, out playerInventory);
                    playerInventory.Initialize();
                }

                return playerInventory;
            }
        }
        
        public MapCropsDataSO MapCropsData
        {
            get
            {
                if (mapCropsData == null)
                {
                    JsonDataManager.LoadData(MapCropsDataSO.PersistentDataName, out mapCropsData);
                }

                return mapCropsData;
            }
        }
        
        public MapTilesDataSO MapTilesData
        {
            get
            {
                if (mapTilesData == null)
                {
                    JsonDataManager.LoadData(MapTilesDataSO.PersistentDataName, out mapTilesData);
                }

                return mapTilesData;
            }
        }
        
        public MapResourcesDataSO MapResourcesData
        {
            get
            {
                if (mapResourcesData == null)
                {
                    JsonDataManager.LoadData(MapResourcesDataSO.PersistentDataName, out mapResourcesData);
                }

                return mapResourcesData;
            }
        }
    }
}