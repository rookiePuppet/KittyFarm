using UnityEngine;

namespace KittyFarm.Data
{
    [CreateAssetMenu(fileName = "NewResourceData", menuName = "Data/Resource Data")]
    public class ResourceDataSO: ScriptableObject
    {
        public int Id;
        public string ResourceName;
        public int RegenerationTime;
        public FarmProductDataSO ProductData;
        public Vector3[] ProductPositions;
    }
}