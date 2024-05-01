using KittyFarm.CropSystem;
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

        private readonly UsableItemSet usableItemSet = new();
        private ItemDataSO harvestTool;

        private void Awake()
        {
            harvestTool = ScriptableObject.CreateInstance<ItemDataSO>();
            harvestTool.Type = ItemType.HarvestTool;
        }

        public Item SpawnItemAt(Vector3 position, ItemDataSO itemData, int amount = 1)
        {
            position.z = 0;

            var itemObj = Instantiate(itemPrefab, position, Quaternion.identity);
            var item = itemObj.GetComponent<Item>();
            item.Initialize(itemData, amount);

            return item;
        }

        public Item SpawnItemAt(Transform parent, Vector3 position, ItemDataSO itemData, int amount = 1)
        {
            position.z = 0;

            var itemObj = Instantiate(itemPrefab, parent);
            itemObj.transform.localPosition = position;
            var item = itemObj.GetComponent<Item>();
            item.Initialize(itemData, amount);

            return item;
        }

        public UsableItem TakeUsableItem(ItemDataSO itemData, Vector3 worldPosition, Vector3Int cellPosition)
        {
            return usableItemSet.TakeUsableItem(itemData, worldPosition, cellPosition);
        }

        public UsableItem TakeHarvestTool(IHarvestable harvestable, Vector3 worldPosition)
        {
            var tool = usableItemSet.TakeUsableItem(harvestTool, worldPosition, Vector3Int.zero) as HarvestTool;
            tool!.HarvestTarget = harvestable;
            return tool;
        }
        
    }
}