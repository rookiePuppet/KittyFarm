using System;
using KittyFarm.InventorySystem;
using KittyFarm.Service;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace KittyFarm.UI
{
    public class ItemSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private Image itemIcon;
        [SerializeField] private TextMeshProUGUI itemCountText;
        [SerializeField] private GameObject selectedBackgroundObject;

        private ItemSlotGroup Group { get; set; }
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

                DataChanged?.Invoke();
            }
        }

        public ItemDataSO ItemData { get; private set; }

        private event Action DataChanged;

        private InventoryItem item;
        private bool isSelected;

        private Button slotButton;

        private RectTransform rectTransform;

        private void Awake()
        {
            slotButton = GetComponent<Button>();
            rectTransform = GetComponent<RectTransform>();

            DataChanged += Refresh;
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

        public void OnDrag(PointerEventData eventData)
        {
            if (IsEmpty) return;
            Group.DragItemSlot(eventData);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (IsEmpty) return;
            Group.BeginDragItemSlot(eventData, itemIcon.sprite);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (IsEmpty) return;
            Group.EndDragItemSlot(eventData);
        }
    }
}