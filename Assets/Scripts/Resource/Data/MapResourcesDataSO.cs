using System.Collections.Generic;
using UnityEngine;

namespace KittyFarm.CropSystem
{
    [CreateAssetMenu(fileName = "MapResourcesData", menuName = "Kitty Farm/Map Resources Data")]
    public class MapResourcesDataSO : ScriptableObject
    {
        public const string PersistentDataName = "MapResourcesData";
        [SerializeField] private List<ResourceGrowthDetails> resourcesDetailsList;

        public List<ResourceGrowthDetails> ResourcesDetailsList => resourcesDetailsList;

        public ResourceGrowthDetails GetGrowthDetails(Vector3 position)
            => resourcesDetailsList.Find(item => item.Position == position);

        public void AddGrowthDetails(ResourceGrowthDetails growthDetails)
        {
            resourcesDetailsList.Add(growthDetails);
        }
    }
}