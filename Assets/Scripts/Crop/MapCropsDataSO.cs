using System.Collections.Generic;
using System.Linq;
using KittyFarm.Data;
using UnityEngine;

namespace KittyFarm.CropSystem
{
    public class MapCropsDataSO : ScriptableObject
    {
        [SerializeField] private List<CropGrowthDetails> growthDetails = new();

        public IEnumerable<CropGrowthDetails> GrowthDetails => growthDetails;

        public void RemoveCropData(CropGrowthDetails details)
        {
            growthDetails.Remove(details);
        }

        public void SaveCropData(CropGrowthDetails details)
        {
            growthDetails.Add(details);
        }

        public void LoadData(MapCropsData data)
        {
            growthDetails.Clear();
            growthDetails.AddRange(data.CropDetailsList);
        }
    }
}