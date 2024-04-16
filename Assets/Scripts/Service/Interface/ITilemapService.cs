using System;
using System.Collections.Generic;
using KittyFarm.Map;
using KittyFarm.UI;
using UnityEngine;
using UnityEngine.Tilemaps;

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
        public Vector3Int WorldToCell(Vector3 worldPosition);
        public Vector3 GetCellCenterWorld(Vector3Int cellPosition);
    }
}