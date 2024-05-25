using System.Collections.Generic;
using KittyFarm.InventorySystem;
using UnityEngine;

namespace KittyFarm.Data
{
    public class MapItemsDataSO : ScriptableObject
    {
        public const string PersistentDataName = "MapItemData";
        
        [SerializeField] private List<MapItem> itemList = new();

        public IEnumerable<MapItem> ItemList => itemList;

        public void AddItem(Vector3 position, int itemId, int count)
        {
            var newItem = new MapItem(position, itemId, count);
            itemList.Add(newItem);
            Debug.Log($"Add item at {newItem.Position}");
        }

        public void RemoveItem(MapItem item)
        {
            itemList.Remove(item);
        }

        public MapItem GetItem(Vector3 position)
        {
            return itemList.Find(item => item.Position == position);
        }
    }
}