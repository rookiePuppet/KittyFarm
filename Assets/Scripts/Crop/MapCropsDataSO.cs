using System.Collections.Generic;
using FrameWork;
using KittyFarm.Data;

namespace KittyFarm.CropSystem
{
    public class MapCropsDataSO : ScriptableData
    {
        public IEnumerable<CropGrowthDetails> GrowthDetails => data.CropDetailsList;
        public override string DataFileName => "MapCropsData";
        
        private MapCropsData data;

        public void RemoveCrop(CropGrowthDetails details)
        {
            data.CropDetailsList.Remove(details);
            SaveData();
        }

        public void AddCrop(CropGrowthDetails details)
        {
            data.CropDetailsList.Add(details);
            SaveData();
        }

        public void LoadData(int mapId)
        {
            LoadData(GetDataFileNameFor(mapId));
            data.mapId = mapId;
        }
        
        public override void LoadData(string fileName)
        {
            data = JsonDataManager.LoadData<MapCropsData>(fileName);
        }

        public override void SaveData()
        {
            JsonDataManager.SaveData(data, GetDataFileNameFor(data.mapId));
        }

        private string GetDataFileNameFor(int mapId) => $"{DataFileName}_{mapId}";
    }
}