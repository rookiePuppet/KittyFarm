using UnityEngine;

namespace KittyFarm.Map
{
    [System.Serializable]
    public struct TileProperty
    {
        public Vector3Int Coordinate;
        public TilePropertyType Type;
    }
}

