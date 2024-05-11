using System.Collections.Generic;
using KittyFarm.Data;
using KittyFarm.Time;
using UnityEngine;

namespace KittyFarm.Harvestable
{
    public class ResourceManager : MonoBehaviour
    {
        [SerializeField] private ResourceDatabaseSO resourceDatabase;
        [SerializeField] private MapResourcesDataSO mapResourcesData;

        private readonly List<Resource> resourcesList = new();
        
        private void OnEnable()
        {
            TimeManager.MinutePassed += UpdateResourcesGrowthDetails;
        }

        private void OnDisable()
        {
            TimeManager.MinutePassed -= UpdateResourcesGrowthDetails;
        }

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            mapResourcesData = GameDataCenter.Instance.MapResourcesData;
 
            foreach (var resource in FindObjectsOfType<Resource>())
            {
                resource.GrowthDetails = mapResourcesData.GetGrowthDetails(resource.transform.position);
                resourcesList.Add(resource);
            }

            UpdateResourcesGrowthDetails();
        }

        private void UpdateResourcesGrowthDetails()
        {
            foreach (var resource in resourcesList)
            {
                resource.Refresh();
            }
        }
    }
}