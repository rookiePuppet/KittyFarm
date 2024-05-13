using KittyFarm.Map;
using KittyFarm.UI;
using UnityEngine;

namespace KittyFarm.Service
{
    public interface ITilemapService : IService
    {
        // public IEnumerable<Tilemap> Tilemaps { get; }
        // public IEnumerable<Tuple<Tilemap, TilemapRenderer>> TilemapsWithRenderers { get; }
        public TilePropertiesInfo GetTilePropertiesInfoAt(Vector3Int cellPosition);
        public bool IsNotDroppableAt(Vector3Int cellPosition);
        public bool IsPlantableAt(Vector3Int cellPosition);
        public bool CheckWasDugAt(Vector3Int cellPosition);
        public void DigAt(Vector3Int cellPosition);
        public void RemoveDigAt(Vector3Int cellPosition);
        // public void WaterAt(Vector3Int cellPosition);
        public bool TryGetTileDetailsOn(Vector3Int gridCoordinate, out TileDetails tileDetails);
        public Grid CurrentGrid { get; }
        public Vector3Int WorldToCell(Vector3 worldPosition);
        public Vector3 GetCellCenterWorld(Vector3Int cellPosition);
    }
}