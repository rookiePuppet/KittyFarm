using System.Collections.Generic;
using FrameWork;
using KittyFarm.Data;

namespace KittyFarm.Map
{
    public class MapTilesDataSO : ScriptableData
    {
        public IEnumerable<TileDetails> AllTilesDetails => data.TileDetailsList;
        public override string DataName => "MapTilesData";

        private List<TileDetails> TileDetailsData => data.TileDetailsList;
        private MapTilesData data;

        public void AddTileDetails(TileDetails details)
        {
            TileDetailsData.Add(details);
            SaveData();
        }

        public void LoadData(int mapId)
        {
            LoadData(GetDataNameFor(mapId));
            data.mapId = mapId;
        }

        public override void LoadData(string fileName)
        {
            data = JsonDataManager.LoadData<MapTilesData>(fileName);
        }

        public override void SaveData()
        {
            JsonDataManager.SaveData(data, GetDataNameFor(data.mapId));
        }

        private string GetDataNameFor(int mapId) => $"{DataName}_{mapId}";
    }
}