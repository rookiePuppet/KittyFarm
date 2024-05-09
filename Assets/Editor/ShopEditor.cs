using System;
using System.Collections.Generic;
using System.Linq;
using KittyFarm.Data;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopEditor : EditorWindow
{
    [SerializeField] private VisualTreeAsset m_VisualTreeAsset = default;
    [SerializeField] private VisualTreeAsset listEntry;

    [SerializeField] private ItemDatabaseSO itemDatabase;
    [SerializeField] private ShopDataSO shopData;

    private readonly List<ShopCommodity> shopCommodities = new();
    private VisualElement root => rootVisualElement;

    private ListView listView;
    private Button listAddButton;
    private Button listRemoveButton;
    private ObjectField itemDataField;
    private IntegerField numberField;
    private Toggle infiniteField;
    private Button saveButton;

    private ShopCommodity selectedCommodity;

    [MenuItem("Tools/ShopEditor")]
    public static void ShowExample()
    {
        ShopEditor wnd = GetWindow<ShopEditor>();
        wnd.titleContent = new GUIContent("ShopEditor");
    }

    public void CreateGUI()
    {
        root.Add(m_VisualTreeAsset.Instantiate());

        listView = root.Q<ListView>("ListView");
        listAddButton = listView.Q<Button>(BaseListView.footerAddButtonName);
        listRemoveButton = listView.Q<Button>(BaseListView.footerRemoveButtonName);
        itemDataField = root.Q<ObjectField>("ItemDataField");
        numberField = root.Q<IntegerField>("NumberField");
        infiniteField = root.Q<Toggle>("InfiniteField");
        saveButton = root.Q<Button>("SaveButton");

        LoadShopData();

        listView.makeItem = () => listEntry.Instantiate();
        listView.bindItem = (element, index) =>
        {
            var itemData = shopCommodities[index].ItemData;
            var nameText = element.Q<Label>("Name");
            nameText.text = itemData.ItemName;
            element.Q<VisualElement>("Icon").style.backgroundImage = new StyleBackground(itemData.IconSprite);
        };
        listView.fixedItemHeight = 80;
        listView.itemsSource = shopCommodities;

        listView.selectionChanged += e =>
        {
            selectedCommodity = (ShopCommodity)e.FirstOrDefault();
            if (selectedCommodity == null) return;
            itemDataField.value = selectedCommodity.ItemData;
            numberField.value = selectedCommodity.Quantity;
            infiniteField.value = selectedCommodity.Infinite;
        };

        listView.itemsAdded += e =>
        {
            var index = e.First();
            shopCommodities.RemoveAt(index);
            var itemData = itemDatabase.GetItemData(1);
            var commodity = new ShopCommodity(itemData, 1, false);
            shopCommodities.Add(commodity);
        };

        itemDataField.RegisterValueChangedCallback(e =>
        {
            if (selectedCommodity == null) return;
            selectedCommodity.ItemData = (ItemDataSO)e.newValue;
            listView.RefreshItem(listView.selectedIndex);
        });

        numberField.RegisterValueChangedCallback(e =>
        {
            if (selectedCommodity == null) return;
            selectedCommodity.Quantity = e.newValue;
        });

        infiniteField.RegisterValueChangedCallback(e =>
        {
            if (selectedCommodity == null) return;
            selectedCommodity.Infinite = e.newValue;
        });

        saveButton.clicked += SaveShopData;
    }

    private void LoadShopData()
    {
        foreach (var details in shopData.CommodityList)
        {
            var commodity = new ShopCommodity(itemDatabase.GetItemData(details.ItemId), details.Quantity,
                details.Infinite);
            shopCommodities.Add(commodity);
        }
    }

    private void SaveShopData()
    {
        var list = new List<CommodityDetails>();
        foreach (var commodity in shopCommodities)
        {
            var commodityDetails = new CommodityDetails()
            {
                ItemId = commodity.ItemData.Id,
                Quantity = commodity.Quantity,
                Infinite = commodity.Infinite
            };
            list.Add(commodityDetails);
        }

        shopData.CommodityList = list;
        EditorUtility.SetDirty(shopData);
    }

    private class ShopCommodity
    {
        public ItemDataSO ItemData;
        public int Quantity;
        public bool Infinite;

        public ShopCommodity(ItemDataSO itemData, int quantity, bool infinite)
        {
            ItemData = itemData;
            Quantity = quantity;
            Infinite = infinite;
        }
    }
}