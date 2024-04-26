
using KittyFarm.Data;
using KittyFarm.InventorySystem;
using UnityEngine;

namespace KittyFarm.Service
{
    public class ItemService : MonoBehaviour, IItemService
    {
        [SerializeField] private ItemDatabaseSO itemDatabase;
        [SerializeField] private GameObject itemPrefab;
        
        public ItemDatabaseSO ItemDatabase => itemDatabase;

        public Item SpawnItemAt(Vector3 position, ItemDataSO itemData, int amount = 1)
        {
            position.z = 0;

            var itemObj = Instantiate(itemPrefab, position, Quaternion.identity);
            var item = itemObj.GetComponent<Item>();
            item.Initialize(itemData, amount);

            return item;
        }
    }
}