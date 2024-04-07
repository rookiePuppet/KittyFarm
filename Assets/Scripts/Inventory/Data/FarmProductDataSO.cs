using UnityEngine;

namespace KittyFarm.InventorySystem
{
    [CreateAssetMenu(fileName = "NewFarmProductData", menuName = "Inventory/Farm Product Data")]
    public class FarmProductDataSO : ItemDataSO
    {
        protected override void OnValidate()
        {
            Type = ItemType.FarmProduct;
            base.OnValidate();
        }
    }
}