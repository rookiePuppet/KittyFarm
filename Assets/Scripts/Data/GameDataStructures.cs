using System;
using System.Collections.Generic;
using KittyFarm.CropSystem;
using KittyFarm.InventorySystem;
using KittyFarm.Map;

namespace KittyFarm.Data
{
    [Serializable]
    public class PlayerInventoryData
    {
        public List<InventoryItem> ItemList;
    }

    [Serializable]
    public class MapCropsData
    {
        public int mapId;
        public List<CropGrowthDetails> CropDetailsList;
    }

    [Serializable]
    public class MapTilesData
    {
        public int mapId;
        public List<TileDetails> TileDetailsList;
    }
}