using System.Collections.Generic;
using KittyFarm.CropSystem;
using UnityEngine;

namespace KittyFarm.Data
{
    public class MapCropsDataSO : ScriptableObject
    {
        [SerializeField] private List<CropGrowthDetails> cropDetailsList = new();
        
        public List<CropGrowthDetails> CropDetailsList => cropDetailsList;

        public const string PersistentDataName = "MapCropsData";

        public void RemoveCrop(CropGrowthDetails details)
        {
            cropDetailsList.Remove(details);
        }

        public void AddCrop(CropGrowthDetails details)
        {
            cropDetailsList.Add(details);
        }
    }
}