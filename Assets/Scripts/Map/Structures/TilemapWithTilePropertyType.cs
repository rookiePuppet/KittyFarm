using System;
using UnityEngine.Tilemaps;

namespace KittyFarm.Map
{
    [Serializable]
    public struct TilemapWithTilePropertyType
    {
        public Tilemap Tilemap;
        public TilePropertyType PropertyType;
    }
}