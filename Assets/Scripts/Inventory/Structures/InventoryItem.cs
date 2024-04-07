using System;

namespace KittyFarm.InventorySystem
{
    [Serializable]

    public class InventoryItem
    {
        public ItemDataSO itemData;
        public int count;

        public InventoryItem(ItemDataSO itemData, int count)
        {
            this.itemData = itemData;
            this.count = count;
        }
    }
}