using KittyFarm.Data;
using KittyFarm.Service;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KittyFarm.UI
{
    public class ShopWindow : UIBase
    {
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
        [SerializeField] private Button sellAllButton;

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
                sellAllButton.gameObject.SetActive(value);
                purchaseButton.gameObject.SetActive(!value);
            }
        }

        private void Awake()
        {
            purchaseButton.onClick.AddListener(OnPurchaseButtonClicked);
            sellButton.onClick.AddListener(OnSellButtonClicked);
            closeButton.onClick.AddListener(Hide);
            sellAllButton.onClick.AddListener(OnSellAllButtonClicked);
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

        public override void Show()
        {
            base.Show();
            InputReader.DisableInput();
        }

        public override void Hide()
        {
            base.Hide();
            InputReader.EnableInput();
        }

        private void OnPurchaseButtonClicked()
        {
            var purchaseAmount = counter.Value;
            var quantityNotEnough = !SelectedCommodity.Infinite && SelectedCommodity.Quantity < purchaseAmount;
            if (purchaseAmount <= 0)
            {
                return;
            }

            if (quantityNotEnough)
            {
                UIManager.Instance.ShowMessage("该商品库存不足");
                return;
            }

            var itemData = ItemService.ItemDatabase.GetItemData(SelectedCommodity.ItemId);
            var totalValue = itemData.Value * purchaseAmount;
            var coinsEnough = GameDataCenter.Instance.PlayerData.Coins >= totalValue;
            if (!coinsEnough)
            {
                UIManager.Instance.ShowMessage("金币不足，先卖点东西吧");
                return;
            }

            var playerData = GameDataCenter.Instance.PlayerData;
            var addItemSuccess = playerData.Inventory.AddItem(itemData, purchaseAmount);
            // 背包无剩余空间
            if (!addItemSuccess)
            {
                UIManager.Instance.ShowMessage("背包没有空位了");
                return;
            }

            // 背包有剩余空间
            playerData.OnPurchasedCommodity(itemData, purchaseAmount);
            SelectedCommodity.Quantity -= purchaseAmount;
            SelectedCommodityItem.Initialize(SelectedCommodity);
            UIManager.Instance.ShowMessage($"购买{itemData.ItemName}X{purchaseAmount}，花费{totalValue}金币");
            UIManager.Instance.ShowUI<GetItemView>().Initialize(itemData, purchaseAmount);

            counter.Reset();
            AudioManager.Instance.PlaySoundEffect(GameSoundEffect.Coin);
        }

        private void OnSellAllButtonClicked()
        {
            var sellAmount = DraggedInItem.itemAmount;
            var itemData = DraggedInItem.itemData;
            var playerData = GameDataCenter.Instance.PlayerData;
            playerData.OnSoldItem(itemData, sellAmount);
            playerData.Inventory.RemoveItem(itemData, sellAmount);
            
            UIManager.Instance.ShowMessage(
                $"卖出{itemData.ItemName}X{sellAmount}，收入{itemData.Value * itemData.SoldDiscount * sellAmount}金币");
            AudioManager.Instance.PlaySoundEffect(GameSoundEffect.Coin);

            rightPanel.SetActive(false);
            IsSellMode = false;
        }

        private void OnSellButtonClicked()
        {
            var sellAmount = counter.Value;
            var quantityNotEnough = DraggedInItem.itemAmount < sellAmount;
            if (sellAmount <= 0 || quantityNotEnough)
            {
                return;
            }

            var itemData = DraggedInItem.itemData;
            var playerData = GameDataCenter.Instance.PlayerData;
            playerData.OnSoldItem(itemData, sellAmount);
            playerData.Inventory.RemoveItem(itemData, sellAmount);
            
            UIManager.Instance.ShowMessage(
                $"卖出{itemData.ItemName}X{sellAmount}，收入{itemData.Value * itemData.SoldDiscount * sellAmount}金币");
            AudioManager.Instance.PlaySoundEffect(GameSoundEffect.Coin);
            
            counter.Reset();
            rightPanel.SetActive(false);
            IsSellMode = false;
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