using KittyFarm.InventorySystem;
using UnityEngine;

namespace KittyFarm.Data
{
    [CreateAssetMenu(fileName = "NewFarmProductData", menuName = "Data/Farm Product Data")]
    public class FarmProductDataSO : ItemDataSO
    {
        protected override void OnValidate()
        {
            Type = ItemType.FarmProduct;
            base.OnValidate();
        }
    }
}