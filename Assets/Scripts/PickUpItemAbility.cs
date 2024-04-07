using KittyFarm.InventorySystem;
using UnityEngine;

namespace KittyFarm
{
    public class PickUpItemAbility : MonoBehaviour
    {
        private PlayerInventorySO inventory;

        private void Awake()
        {
            inventory = GetComponentInParent<PlayerController>().Inventory;
        }

        private void PickUpItem(Item item)
        {
            var isItemAdded = inventory.AddItem(item);
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