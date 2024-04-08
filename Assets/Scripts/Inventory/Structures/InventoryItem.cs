using System;

namespace KittyFarm.InventorySystem
{
    [Serializable]
    public class InventoryItem
    {
        public int itemId;
        public int count;

        public InventoryItem()
        {
        }

        public InventoryItem(int itemId, int count)
        {
            this.itemId = itemId;
            this.count = count;
        }
    }
}