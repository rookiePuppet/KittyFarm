using System;
using System.Collections.Generic;
using KittyFarm.UI;
using UnityEngine;

namespace KittyFarm.Data
{
    [CreateAssetMenu(fileName = "ShopData", menuName = "Data/Shop Data")]
    public class ShopDataSO : ScriptableObject
    {
        public const string PersistentDataName = "ShopData";
        
        [SerializeField] private List<CommodityDetails> commodityList;

        public IEnumerable<CommodityDetails> CommodityList => commodityList;

        private void OnEnable()
        {
            ShopWindow.PurchasedCommodity += OnPurchasedCommodity;
        }

        private void OnPurchasedCommodity(ItemDataSO itemData, int amount)
        {
            var details = commodityList.Find(details => details.ItemData == itemData);
            details.Quantity -= amount;
        }
    }

    [Serializable]
    public class CommodityDetails
    {
        public ItemDataSO ItemData;
        public int Quantity;
        public bool Infinite;
    }
}