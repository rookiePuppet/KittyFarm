using System.Collections.Generic;
using System.Linq;
using Framework;
using KittyFarm.CropSystem;
using KittyFarm.InventorySystem;
using UnityEngine;

namespace KittyFarm.Data
{
    public class GameDataManager : MonoSingleton<GameDataManager>
    {
        [SerializeField] private PlayerInventorySO playerInventory;
        [SerializeField] private MapCropsDataSO mapCropsData;

        private void Start()
        {
            LoadPlayerInventoryData();
            LoadMapCropsData();
        }

        private void OnApplicationQuit()
        {
            SavePlayerInventoryData();
            SaveMapCropsData();
        }

        private void LoadPlayerInventoryData()
        {
            var data = JsonDataManager.Instance.LoadData<PlayerInventoryData>() ?? new PlayerInventoryData
                { ItemList = new List<InventoryItem>()};

            playerInventory.LoadData(data);
        }

        private void SavePlayerInventoryData()
        {
            var inventoryItems = playerInventory.AllItems.ToList();
            JsonDataManager.Instance.SaveData(new PlayerInventoryData { ItemList = inventoryItems });
        }

        private void LoadMapCropsData()
        {
            var data = JsonDataManager.Instance.LoadData<MapCropsData>() ?? new MapCropsData()
            {
                CropDetailsList = new List<CropGrowthDetails>()
            };
            
            mapCropsData.LoadData(data);
        }

        private void SaveMapCropsData()
        {
            var cropsData = mapCropsData.GrowthDetails.ToList();
            JsonDataManager.Instance.SaveData(new MapCropsData() {CropDetailsList = cropsData});
        }
    }
}