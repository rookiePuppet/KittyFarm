using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KittyFarm.Data
{
    [CreateAssetMenu(fileName = "ShopData", menuName = "Data/Shop Data")]
    public class ShopDataSO : ScriptableObject
    {
        public const string PersistentDataName = "ShopData";
        
        [SerializeField] private List<CommodityDetails> commodityList;

        public IEnumerable<CommodityDetails> CommodityList
        {
            get => commodityList;
            set => commodityList = value.ToList();
        }
    }

    [Serializable]
    public class CommodityDetails
    {
        public int ItemId;
        public int Quantity;
        public bool Infinite;
    }
}