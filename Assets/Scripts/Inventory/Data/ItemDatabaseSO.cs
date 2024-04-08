using System.Collections.Generic;
using UnityEngine;

namespace KittyFarm.InventorySystem
{
    [CreateAssetMenu(fileName = "ItemDatabase", menuName = "Kitty Farm/Item Database")]
    public class ItemDatabaseSO : ScriptableObject
    {
        [field: SerializeField] private List<ItemDataSO> ItemDataList { get; set; }

        public ItemDataSO GetItemData(int id) => ItemDataList.Find(itemData => itemData.Id == id);
    }
}