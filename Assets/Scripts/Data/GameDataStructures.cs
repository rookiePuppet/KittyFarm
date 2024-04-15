using System;
using System.Collections.Generic;
using KittyFarm.CropSystem;
using KittyFarm.InventorySystem;

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
        public List<CropGrowthDetails> CropDetailsList;
    }
}