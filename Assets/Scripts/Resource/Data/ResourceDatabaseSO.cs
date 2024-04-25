using System.Collections.Generic;
using UnityEngine;

namespace KittyFarm.CropSystem
{
    [CreateAssetMenu(fileName = "ResourceDatabase", menuName = "Kitty Farm/Resource Database")]
    public class ResourceDatabaseSO: ScriptableObject
    {
        [field: SerializeField] private List<ResourceDataSO> ResourceDataList { get; set; }
        
        public ResourceDataSO GetResourceData(int id) => ResourceDataList.Find(resourceData => resourceData.Id == id);
    }
}