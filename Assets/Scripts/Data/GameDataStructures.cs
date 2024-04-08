using System;
using System.Collections.Generic;
using KittyFarm.InventorySystem;

namespace KittyFarm.Data
{
    [Serializable]
    public class PlayerInventoryData
    {
        public List<InventoryItem> ItemList;
    }
}