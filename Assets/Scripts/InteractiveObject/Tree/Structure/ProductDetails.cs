using System;
using KittyFarm.Data;

namespace KittyFarm.InteractiveObject
{
    [Serializable]
    public struct ProductDetails
    {
        public ItemDataSO ProductData;
        public int Quantity;
    }
}