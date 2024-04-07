using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KittyFarm.CropSystem
{
    public class MapCropsDataSO : ScriptableObject
    {
        public List<CropGrowthDetails> GrowthDetails = new();

        public void SaveCropData()
        {
            GrowthDetails.Clear();

            var growthDetails = SearchAllCrop().Select(crop => crop.GrowthDetails);
            GrowthDetails.AddRange(growthDetails);
        }

        public void SaveCropData(CropGrowthDetails growthDetails)
        {
            GrowthDetails.Add(growthDetails);
        }

        private IEnumerable<Crop> SearchAllCrop()
        {
            var allCrops = FindObjectsOfType<Crop>();
            return allCrops;
        }
    }
}