using KittyFarm.InventorySystem;
using KittyFarm.Service;
using UnityEngine;

namespace KittyFarm
{
    public class SeedUsable : IUsableItem
    {
        public bool CanUse { get; }
        private readonly SeedDataSO data;
        private readonly Vector3 worldPosition;
        private readonly Vector3Int cellPosition;

        private readonly ICropService cropService;

        public SeedUsable(Vector3 worldPosition, Vector3Int cellPosition, SeedDataSO seedData)
        {
            this.worldPosition = worldPosition;
            this.cellPosition = cellPosition;
            data = seedData;

            var tilemapService = ServiceCenter.Get<ITilemapService>();
            cropService = ServiceCenter.Get<ICropService>();

            var wasDugAtCell = tilemapService.CheckWasDugAt(cellPosition);
            var existsCropAtCell = cropService.IsCropExistentAt(cellPosition);

            CanUse = wasDugAtCell && !existsCropAtCell;
        }

        public void Use()
        {
            if (!CanUse) return;

            cropService.PlantCrop(data.CropData, cellPosition);
        }
    }
}