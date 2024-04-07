using System.Linq;
using UnityEngine;

namespace KittyFarm.Map
{
    public class MapPropertiesDataSO : ScriptableObject
    {
        public TileProperties[] Data;
        
        private TileProperties GetProperties(TilePropertyType propertyType) =>
            Data[(int)propertyType];

        public bool IsPlantable(Vector3Int coordinate)
        {
            var properties = GetProperties(TilePropertyType.Plantable).Properties;
            return properties.Any(property => property.Coordinate == coordinate);
        }

        public bool IsNotDroppable(Vector3Int coordinate)
        {
            var properties = GetProperties(TilePropertyType.NotDroppable).Properties;
            return properties.Any(property => property.Coordinate == coordinate);
        }
    }
}