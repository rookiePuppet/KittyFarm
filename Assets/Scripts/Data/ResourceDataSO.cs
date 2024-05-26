using KittyFarm.InteractiveObject;
using UnityEngine;

namespace KittyFarm.Data
{
    [CreateAssetMenu(fileName = "NewResourceData", menuName = "Data/Resource Data")]
    public class ResourceDataSO: ScriptableObject
    {
        public string ResourceName;
        public int RegenerationTime = 1;
        public ProductDetails ProductDetails;
    }
}