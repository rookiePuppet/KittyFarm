using System;
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

        private bool isInitialized;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            if (itemData != null && !isInitialized) Initialize(itemData);
        }

        public void Initialize(ItemDataSO data, int amount = 1)
        {
            itemData = data;
            count = amount;

            spriteRenderer.sprite = data.IconSprite;

            isInitialized = true;
        }

        [ContextMenu("Load Sprite")]
        private void LoadSprite()
        {
            if (itemData != null)
            {
                GetComponent<SpriteRenderer>().sprite = itemData.IconSprite;
            }
        }
    }
}