using System;
using KittyFarm.Data;
using KittyFarm.InventorySystem;
using KittyFarm.Service;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KittyFarm.UI
{
    public class ItemSlot : MonoBehaviour
    {
        public static event Action<ItemSlot> OnClicked;
        
        [SerializeField] private Image itemIcon;
        [SerializeField] private TextMeshProUGUI itemCountText;
        [SerializeField] private GameObject selectedBackgroundObject;

        public int Index { get; set; }
        public bool IsEmpty { get; private set; }
        public ItemDataSO ItemData { get; private set; }
        
        private InventoryItem item;
        private bool isSelected;
        private Button slotButton;

        private void Awake()
        {
            slotButton = GetComponent<Button>();
        }

        private void OnEnable()
        {
            slotButton.onClick.AddListener(OnClickedEvent);
        }
        
        private void OnDisable()
        {
            slotButton.onClick.RemoveListener(OnClickedEvent);
        }
        
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
                ItemData = IsEmpty ? null : ServiceCenter.Get<IItemService>().ItemDatabase.GetItemData(item.itemId);
                Refresh();
            }
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

        private void OnClickedEvent()
        {
            OnClicked?.Invoke(this);
        }
    }
}