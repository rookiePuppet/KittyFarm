using KittyFarm.InventorySystem;
using UnityEngine;

namespace KittyFarm.Data
{
    [CreateAssetMenu(fileName = "NewFarmProductData", menuName = "Data/Farm Product Data")]
    public class FarmProductDataSO : ItemDataSO
    {
        protected void OnValidate()
        {
            Type = ItemType.FarmProduct;
        }
    }
}