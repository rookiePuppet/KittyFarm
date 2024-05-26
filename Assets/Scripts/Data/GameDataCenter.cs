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
        [SerializeField] private PlayerDataSO playerData;
        [SerializeField] private ShopDataSO shopData;

        public SettingsDataSO SettingsData => settingsData;
        
        public PlayerDataSO PlayerData => playerData;
        public ShopDataSO ShopData => shopData;
        public PlayerInventory PlayerInventory => PlayerData.Inventory;
        public MapDataSO MapData => mapData;
        public MapCropsDataSO MapCropsData => mapCropsData;
        public MapTilesDataSO MapTilesData => mapTilesData;
        public MapItemsDataSO MapItemsData => mapItemsData;

        private MapCropsDataSO mapCropsData;
        private MapTilesDataSO mapTilesData;
        private MapItemsDataSO mapItemsData;
        
        private SettingsDataSO settingsData;

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

            JsonDataManager.LoadData(MapCropsDataSO.PersistentDataName, out mapCropsData);
            JsonDataManager.LoadData(MapTilesDataSO.PersistentDataName, out mapTilesData);
            JsonDataManager.LoadData(MapItemsDataSO.PersistentDataName, out mapItemsData);

            if (JsonDataManager.Exists<ShopDataSO>(ShopDataSO.PersistentDataName))
            {
                JsonDataManager.LoadData(ShopDataSO.PersistentDataName, out shopData);
            }
            
            JsonDataManager.LoadData(SettingsDataSO.PersistentDataName, out settingsData);
        }

        private void OnBeforeGameExit()
        {
            BeforeSaveData?.Invoke();

            JsonDataManager.SaveData(PlayerDataSO.PersistentDataName, playerData);
            JsonDataManager.SaveData(MapCropsDataSO.PersistentDataName, mapCropsData);
            JsonDataManager.SaveData(MapTilesDataSO.PersistentDataName, mapTilesData);
            JsonDataManager.SaveData(MapItemsDataSO.PersistentDataName, mapItemsData);
            JsonDataManager.SaveData(ShopDataSO.PersistentDataName, shopData);
            JsonDataManager.SaveData(SettingsDataSO.PersistentDataName, settingsData);
        }
    }
}