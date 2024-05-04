using System;
using KittyFarm.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KittyFarm.UI
{
    public class ShopWindow : UIBase
    {
        public static event Action<ItemDataSO, int> PurchasedCommodity;
        
        [Header("商品格子预制体")]
        [SerializeField] private GameObject commodityItemPrefab;
        [Space]
        [SerializeField] private Button closeButton;
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private GameObject rightPanel;
        [SerializeField] private Image commodityImage;
        [SerializeField] private TextMeshProUGUI commodityNameText;
        [SerializeField] private TextMeshProUGUI commodityDescriptionText;
        [SerializeField] private TextMeshProUGUI commodityPriceText;
        [SerializeField] private CounterController counter;
        [SerializeField] private Button purchaseButton;

        private ShopDataSO Data => GameDataCenter.Instance.ShopData;

        private CommodityItem SelectedCommodityItem { get; set; }
        private CommodityDetails SelectedCommodity => SelectedCommodityItem.Details;

        private void Awake()
        {
            purchaseButton.onClick.AddListener(OnPurchaseButtonClicked);
            closeButton.onClick.AddListener(Hide);
        }

        private void OnEnable()
        {
            CommodityItem.Clicked += OnCommodityItemClicked;
            rightPanel.SetActive(false);
        }

        private void OnDisable()
        {
            CommodityItem.Clicked -= OnCommodityItemClicked;
        }

        private void Start()
        {
            foreach (var commodity in Data.CommodityList)
            {
                var itemObj = Instantiate(commodityItemPrefab, scrollRect.content);
                var item = itemObj.GetComponent<CommodityItem>();
                item.Initialize(commodity);
            }
        }

        private void OnPurchaseButtonClicked()
        {
            var purchaseAmount = counter.Value;
            var quantityNotEnough = !SelectedCommodity.Infinite && SelectedCommodity.Quantity < purchaseAmount;
            if (purchaseAmount <= 0 || quantityNotEnough)
            {
                return;
            }

            var itemData = SelectedCommodity.ItemData;

            var totalValue = itemData.Value * purchaseAmount;
            if (GameDataCenter.Instance.PlayerData.Coins >= totalValue)
            {
                PurchasedCommodity?.Invoke(itemData, purchaseAmount);
                
                SelectedCommodityItem.Initialize(SelectedCommodity);
            }
        }

        private void OnCommodityItemClicked(CommodityItem commodityItem)
        {
            SelectedCommodityItem = commodityItem;
            
            var commodityDetails = commodityItem.Details;
            commodityImage.sprite = commodityDetails.ItemData.IconSprite;
            commodityNameText.text = commodityDetails.ItemData.ItemName;
            commodityDescriptionText.text = commodityDetails.ItemData.Description;
            commodityPriceText.text = commodityDetails.ItemData.Value.ToString();

            rightPanel.SetActive(true);
        }
    }
}