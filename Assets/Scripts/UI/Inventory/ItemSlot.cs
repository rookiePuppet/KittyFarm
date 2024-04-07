using System;
using KittyFarm.InventorySystem;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace KittyFarm.UI
{
    public class ItemSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] public Image itemIcon;
        [SerializeField] public TextMeshProUGUI itemCountText;
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
                IsEmpty = item.count == 0 && item.itemData == null;
                DataChanged?.Invoke();
            }
        }

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

            itemIcon.sprite = item.itemData.IconSprite;
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