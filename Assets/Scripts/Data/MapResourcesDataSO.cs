using System.Collections.Generic;
using KittyFarm.CropSystem;
using UnityEngine;

namespace KittyFarm.Data
{
    [CreateAssetMenu(fileName = "MapResourcesData", menuName = "Data/Map Resources Data")]
    public class MapResourcesDataSO : ScriptableObject
    {
        public const string PersistentDataName = "MapResourcesData";
        [SerializeField] private List<ResourceGrowthDetails> resourcesDetailsList = new();

        public List<ResourceGrowthDetails> ResourcesDetailsList => resourcesDetailsList;

        public ResourceGrowthDetails GetGrowthDetails(Vector3 position)
            => resourcesDetailsList.Find(item => item.Position == position);

        public void AddGrowthDetails(ResourceGrowthDetails growthDetails)
        {
            resourcesDetailsList.Add(growthDetails);
        }
    }
}