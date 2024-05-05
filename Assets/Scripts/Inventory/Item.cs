using KittyFarm.Data;
using UnityEngine;

namespace KittyFarm.InventorySystem
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private ItemDataSO itemData;
        [SerializeField] private int count = 1;

        public ItemDataSO ItemData => itemData;
        public int Count => count;

        private SpriteRenderer spriteRenderer;
        public Vector3 InherentPosition { get; private set; }

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            InherentPosition = transform.position;
        }

        public void Initialize(ItemDataSO data, int amount = 1)
        {
            itemData = data;
            count = amount;

            spriteRenderer.sprite = data.IconSprite;
        }
    }
}