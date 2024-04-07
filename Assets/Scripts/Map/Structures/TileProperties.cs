using System;
using System.Collections.Generic;

namespace KittyFarm.Map
{
    [Serializable]
    public struct TileProperties
    {
        public TilePropertyType PropertyType;
        public List<TileProperty> Properties;
    }
}