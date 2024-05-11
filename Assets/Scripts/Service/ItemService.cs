
using KittyFarm.Data;
using KittyFarm.Harvestable;
using KittyFarm.InventorySystem;
using KittyFarm.MapClick;
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

        private MapItemsDataSO mapItemsData;

        private void Awake()
        {
            harvestTool = ScriptableObject.CreateInstance<ItemDataSO>();
            harvestTool.Type = ItemType.HarvestTool;
        }

        private void OnEnable()
        {
            GameManager.MapChanged += Initialize;
        }

        private void OnDisable()
        {
            GameManager.MapChanged -= Initialize;
        }

        private void Initialize()
        {
            mapItemsData = GameDataCenter.Instance.MapItemsData;

            foreach (var item in mapItemsData.ItemList)
            {
                SpawnItemAt(item.Position, itemDatabase.GetItemData(item.ItemId), item.Count);
            }
        }

        public Item SpawnItemAt(Vector3 position, ItemDataSO itemData, int amount = 1)
        {
            position.z = 0;

            var itemObj = Instantiate(itemPrefab, position, Quaternion.identity);
            var item = itemObj.GetComponent<Item>();
            item.Initialize(itemData, amount);

            if (GetMapItem(position) == null)
            {
                AddMapItem(position, itemData.Id, amount);
            }

            return item;
        }

        public Item SpawnItemAt(Transform parent, Vector3 position, ItemDataSO itemData, int amount = 1)
        {
            position.z = 0;

            var itemObj = Instantiate(itemPrefab, parent);
            itemObj.transform.localPosition = position;
            var item = itemObj.GetComponent<Item>();
            item.Initialize(itemData, amount);

            if (GetMapItem(position) == null)
            {
                AddMapItem(position, itemData.Id, amount);
            }

            return item;
        }

        public void AddMapItem(Vector3 position, int itemId, int amount)
        {
            mapItemsData.AddItem(position, itemId, amount);
        }

        public void RemoveMapItem(Vector3 position)
        {
            mapItemsData.RemoveItem(mapItemsData.GetItem(position));
        }

        public MapItem GetMapItem(Vector3 position)
        {
            return mapItemsData.GetItem(position);
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