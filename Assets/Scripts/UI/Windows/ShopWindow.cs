using System;
using KittyFarm.Data;
using KittyFarm.Service;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KittyFarm.UI
{
    public class ShopWindow : UIBase
    {
        public static event Action<ItemDataSO, int> PurchasedCommodity;
        public static event Action<ItemDataSO, int> SoldItem;

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
        [SerializeField] private Button sellButton;

        private ShopDataSO Data => GameDataCenter.Instance.ShopData;

        private CommodityItem SelectedCommodityItem { get; set; }
        private CommodityDetails SelectedCommodity => SelectedCommodityItem.Details;
        private (ItemDataSO itemData, int itemAmount) DraggedInItem { get; set; }

        private IItemService ItemService => ServiceCenter.Get<IItemService>();

        private bool IsSellMode // 是否为卖出物品模式
        {
            set
            {
                sellButton.gameObject.SetActive(value);
                purchaseButton.gameObject.SetActive(!value);
            }
        }

        private void Awake()
        {
            purchaseButton.onClick.AddListener(OnPurchaseButtonClicked);
            sellButton.onClick.AddListener(OnSellButtonClicked);
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

            var itemData = ItemService.ItemDatabase.GetItemData(SelectedCommodity.ItemId);

            var totalValue = itemData.Value * purchaseAmount;
            if (GameDataCenter.Instance.PlayerData.Coins >= totalValue)
            {
                PurchasedCommodity?.Invoke(itemData, purchaseAmount);

                SelectedCommodity.Quantity -= purchaseAmount;
                SelectedCommodityItem.Initialize(SelectedCommodity);
            }
            
            counter.Reset();
            AudioManager.Instance.PlaySoundEffect(GameSoundEffect.Coin);
        }

        private void OnSellButtonClicked()
        {
            var sellAmount = counter.Value;
            var quantityNotEnough = DraggedInItem.itemAmount < sellAmount;
            if (sellAmount <= 0 || quantityNotEnough)
            {
                return;
            }
            
            SoldItem?.Invoke(DraggedInItem.itemData, sellAmount);
            counter.Reset();
            AudioManager.Instance.PlaySoundEffect(GameSoundEffect.Coin);
        }

        private void OnCommodityItemClicked(CommodityItem commodityItem)
        {
            SelectedCommodityItem = commodityItem;

            var itemData = ItemService.ItemDatabase.GetItemData(commodityItem.Details.ItemId);
            commodityImage.sprite = itemData.IconSprite;
            commodityNameText.text = itemData.ItemName;
            commodityDescriptionText.text = itemData.Description;
            commodityPriceText.text = itemData.Value.ToString();

            rightPanel.SetActive(true);
            IsSellMode = false;
        }

        public void OnItemDraggedIn(ItemDataSO itemData, int itemAmount)
        {
            DraggedInItem = (itemData, itemAmount);
            commodityImage.sprite = DraggedInItem.itemData.IconSprite;
            commodityNameText.text = DraggedInItem.itemData.ItemName;
            commodityDescriptionText.text = DraggedInItem.itemData.Description;
            commodityPriceText.text = $"{DraggedInItem.itemData.Value * DraggedInItem.itemData.SoldDiscount}";

            rightPanel.SetActive(true);
            IsSellMode = true;
        }
    }
}