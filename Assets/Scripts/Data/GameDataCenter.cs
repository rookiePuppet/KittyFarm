using Framework;
using FrameWork;
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

        private PlayerInventorySO playerInventory;
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

        private void Start()
        {
            LoadPlayerInventory();
            JsonDataManager.LoadData(MapCropsDataSO.PersistentDataName, out mapCropsData);
            JsonDataManager.LoadData(MapTilesDataSO.PersistentDataName, out mapTilesData);
        }

        private void LoadPlayerInventory()
        {
            JsonDataManager.LoadData(PlayerInventorySO.PersistentDataName, out playerInventory);
            if (playerInventory.Items.Count != 0) return;
            for (var index = 0; index < PlayerInventorySO.MaxSize; index++)
            {
                playerInventory.Items.Add(new InventoryItem());
            }
        }

        private void OnBeforeGameExit()
        {
            JsonDataManager.SaveData(PlayerInventorySO.PersistentDataName, playerInventory);
            JsonDataManager.SaveData(MapCropsDataSO.PersistentDataName, mapCropsData);
            JsonDataManager.SaveData(MapTilesDataSO.PersistentDataName, mapTilesData);
        }
    }
}