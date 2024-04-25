using KittyFarm.InventorySystem;
using UnityEngine;

namespace KittyFarm.CropSystem
{
    [CreateAssetMenu(fileName = "NewResourceData", menuName = "Kitty Farm/Resource Data")]
    public class ResourceDataSO: ScriptableObject
    {
        public int Id;
        public string ResourceName;
        public int RegenerationTime;
        public FarmProductDataSO ProductData;
    }
}