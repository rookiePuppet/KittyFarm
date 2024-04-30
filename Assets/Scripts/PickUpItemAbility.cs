using KittyFarm.Data;
using KittyFarm.InventorySystem;
using UnityEngine;

namespace KittyFarm
{
    public class PickUpItemAbility : MonoBehaviour
    {
        private PlayerInventory Inventory => GameDataCenter.Instance.PlayerInventory;

        private void PickUpItem(Item item)
        {
            var isItemAdded = Inventory.AddItem(item);
            if (isItemAdded)
            {
                Destroy(item.gameObject);
            }
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Item")) return;

            var item = other.GetComponent<Item>();
            PickUpItem(item);
        }
    }
}