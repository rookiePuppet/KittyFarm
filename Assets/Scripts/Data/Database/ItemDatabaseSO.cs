using KittyFarm.InventorySystem;
using UnityEngine;

namespace KittyFarm.Data
{
    [CreateAssetMenu(fileName = "ItemDatabase", menuName = "Database/Item Database")]
    public class ItemDatabaseSO : DatabaseSO<ItemDataSO>
    {
        public ItemDataSO GetItemData(int id) => dataList.Find(itemData => itemData.Id == id);
    }
}