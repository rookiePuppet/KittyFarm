using System;
using KittyFarm.Data;
using KittyFarm.InventorySystem;
using KittyFarm.Service;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace KittyFarm.UI
{
    public class ItemSlot : MonoBehaviour
    {
        [SerializeField] private Image itemIcon;
        [SerializeField] private TextMeshProUGUI itemCountText;
        [SerializeField] private GameObject selectedBackgroundObject;

        public int Index { get; private set; }
        public bool IsEmpty { get; private set; }
        public bool IsSelected
        {
            get => isSelected;
            set
            {
                isSelected = value;
                selectedBackgroundObject.SetActive(isSelected);
            }
        }
        public InventoryItem Item
        {
            get => item;
            set
            {
                item = value;
                IsEmpty = item.itemId <= 0 || item.count == 0;

                if (!IsEmpty)
                {
                    ItemData = ServiceCenter.Get<IItemService>().ItemDatabase.GetItemData(item.itemId);
                }

                Refresh();
            }
        }
        public ItemDataSO ItemData { get; private set; }
        public Sprite ItemSprite => itemIcon.sprite;

        private ItemSlotGroup Group { get; set; }
        private InventoryItem item;
        private bool isSelected;
        private Button slotButton;
        private RectTransform rectTransform;

        private void Awake()
        {
            slotButton = GetComponent<Button>();
            rectTransform = GetComponent<RectTransform>();
        }

        public void Initialize(ItemSlotGroup group, int index)
        {
            Group = group;
            Index = index;
            slotButton.onClick.AddListener(OnClicked);
        }

        private void Refresh()
        {
            if (IsEmpty)
            {
                itemIcon.enabled = false;
                itemCountText.text = "";
                return;
            }

            itemIcon.sprite = ItemData.IconSprite;
            itemIcon.enabled = true;
            itemCountText.text = item.count.ToString();
        }

        private void OnClicked()
        {
            Group.UpdateItemSlotSelected(this);
        }
    }
}