using System.Collections.Generic;
using Framework;
using KittyFarm.CropSystem;
using KittyFarm.InventorySystem;
using KittyFarm.Map;
using UnityEngine;

namespace KittyFarm.Data
{
    public class GameDataCenter : MonoSingleton<GameDataCenter>
    {
        [SerializeField] private MapDatabaseSO mapDatabase;
        
        public PlayerInventorySO PlayerInventory { get; private set; }

        private readonly Dictionary<int, MapCropsDataSO> mapCropsDataDic = new();
        private readonly Dictionary<int, MapTilesDataSO> mapTilesDataDic = new();

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
            PlayerInventory = ScriptableObject.CreateInstance<PlayerInventorySO>();
            PlayerInventory.LoadData();
        }

        private void OnBeforeGameExit()
        {
            foreach (var cropsData in mapCropsDataDic.Values)
            {
                cropsData.SaveData();
            }

            foreach (var tilesData in mapTilesDataDic.Values)
            {
                tilesData.SaveData();
            }
        }
        
        public MapDataSO GetMapData(int mapId) => mapDatabase.GetMapData(mapId);

        public MapCropsDataSO GetMapCropsData(int mapId)
        {
            if (!mapCropsDataDic.TryGetValue(mapId, out var data))
            {
                data = ScriptableObject.CreateInstance<MapCropsDataSO>();
                data.LoadData(mapId);

                mapCropsDataDic[mapId] = data;
            }

            return data;
        }
        
        public MapTilesDataSO GetMapTilesData(int mapId)
        {
            if (!mapTilesDataDic.TryGetValue(mapId, out var data))
            {
                data = ScriptableObject.CreateInstance<MapTilesDataSO>();
                data.LoadData(mapId);

                mapTilesDataDic[mapId] = data;
            }

            return data;
        }
    }
}