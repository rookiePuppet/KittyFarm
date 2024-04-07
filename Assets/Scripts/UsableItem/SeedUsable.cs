using KittyFarm.InventorySystem;
using KittyFarm.Service;
using UnityEngine;

namespace KittyFarm
{
    public class SeedUsable : IUsableItem
    {
        private readonly SeedDataSO data;

        public SeedUsable(SeedDataSO seedData)
        {
            data = seedData;
        }

        public void Use(Vector3 worldPosition, Vector3Int cellPosition)
        {
            var wasDugAtCell = ServiceCenter.Get<IMapService>().CheckWasDugAt(cellPosition);
            if (!wasDugAtCell) return;
            
            var cropService = ServiceCenter.Get<ICropService>();
            var existsCropAtCell = cropService.CheckCropExistsAt(cellPosition);
            
            if (existsCropAtCell)
            {
                Debug.Log("Crop already exists at this position");
                return;
            }
            
            cropService.PlantCrop(data.CropData, cellPosition);
        }
    }
}