using System;
using KittyFarm.Data;
using KittyFarm.Service;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KittyFarm.UI
{
    public class CommodityItem : MonoBehaviour
    {
        public static event Action<CommodityItem> Clicked;

        [SerializeField] private Image iconImage;
        [SerializeField] private TextMeshProUGUI amountText;

        private Button button;

        public CommodityDetails Details { get; private set; }

        private void Awake()
        {
            button = GetComponent<Button>();

            button.onClick.AddListener(() => Clicked?.Invoke(this));
        }

        public void Initialize(CommodityDetails commodityDetails)
        {
            Details = commodityDetails;

            iconImage.sprite = ServiceCenter.Get<IItemService>().ItemDatabase.GetItemData(Details.ItemId).IconSprite;
            if (Details.Infinite)
            {
                amountText.enabled = false;
            }
            else
            {
                amountText.enabled = true;
                amountText.text = Details.Quantity.ToString();
            }
        }
    }
}