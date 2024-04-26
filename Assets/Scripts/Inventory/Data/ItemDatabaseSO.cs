using KittyFarm.Data;
using UnityEngine;

namespace KittyFarm.InventorySystem
{
    [CreateAssetMenu(fileName = "ItemDatabase", menuName = "Kitty Farm/Item Database")]
    public class ItemDatabaseSO : DatabaseSO<ItemDataSO>
    {
        public ItemDataSO GetItemData(int id) => dataList.Find(itemData => itemData.Id == id);
    }
}