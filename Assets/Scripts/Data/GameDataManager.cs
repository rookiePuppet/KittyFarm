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

            print(Application.persistentDataPath);
        }

        private void OnApplicationQuit()
        {
            SavePlayerInventoryData();
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
    }
}