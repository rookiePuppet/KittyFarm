using System;
using Framework;
using FrameWork;
using UnityEngine;

namespace KittyFarm.Data
{
    public class GameDataCenter : MonoSingleton<GameDataCenter>
    {
        public static event Action BeforeSaveData;

        [SerializeField] private MapDataSO mapData;
        [SerializeField] private MapResourcesDataSO mapResourcesData;
        [SerializeField] private PlayerDataSO playerData;
        [SerializeField] private ShopDataSO shopData;

        public PlayerDataSO PlayerData => playerData;
        public ShopDataSO ShopData => shopData;
        public PlayerInventory PlayerInventory => PlayerData.Inventory;
        public MapDataSO MapData => mapData;
        public MapResourcesDataSO MapResourcesData => mapResourcesData;
        public MapCropsDataSO MapCropsData => mapCropsData;
        public MapTilesDataSO MapTilesData => mapTilesData;

        private MapCropsDataSO mapCropsData;
        private MapTilesDataSO mapTilesData;

        private void OnEnable()
        {
            GameManager.BeforeGameExit += OnBeforeGameExit;
        }

        private void OnDisable()
        {
            GameManager.BeforeGameExit -= OnBeforeGameExit;
        }

        protected override void Awake()
        {
            base.Awake();

            LoadData();
        }

        private void LoadData()
        {
            if (JsonDataManager.Exists<PlayerDataSO>(PlayerDataSO.PersistentDataName))
            {
                JsonDataManager.LoadData(PlayerDataSO.PersistentDataName, out playerData);
            }
            else
            {
                playerData.Initialize();
            }

            if (JsonDataManager.Exists<MapResourcesDataSO>(MapResourcesDataSO.PersistentDataName))
            {
                JsonDataManager.LoadData(MapResourcesDataSO.PersistentDataName, out mapResourcesData);
            }

            JsonDataManager.LoadData(MapCropsDataSO.PersistentDataName, out mapCropsData);
            JsonDataManager.LoadData(MapTilesDataSO.PersistentDataName, out mapTilesData);

            if (JsonDataManager.Exists<ShopDataSO>(ShopDataSO.PersistentDataName))
            {
                JsonDataManager.LoadData(ShopDataSO.PersistentDataName, out shopData);
            }
        }

        private void OnBeforeGameExit()
        {
            BeforeSaveData?.Invoke();

            JsonDataManager.SaveData(PlayerDataSO.PersistentDataName, playerData);
            JsonDataManager.SaveData(MapCropsDataSO.PersistentDataName, mapCropsData);
            JsonDataManager.SaveData(MapTilesDataSO.PersistentDataName, mapTilesData);
            JsonDataManager.SaveData(MapResourcesDataSO.PersistentDataName, mapResourcesData);
            JsonDataManager.SaveData(ShopDataSO.PersistentDataName, shopData);
        }
    }
}