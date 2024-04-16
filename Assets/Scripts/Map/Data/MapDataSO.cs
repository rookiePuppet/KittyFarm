using System.Linq;
using UnityEngine;

namespace KittyFarm.Map
{
    [CreateAssetMenu(fileName = "MapData", menuName = "Kitty Farm/Map Data")]
    public class MapDataSO : ScriptableObject
    {
        [Header("基础信息")]
        [SerializeField] private int mapId;
        [SerializeField] private string mapName = "Default";
        [SerializeField] private Vector2Int gridOriginCoordinate;
        [SerializeField] private Vector2Int gridSize;

        [Header("地图网格属性数据")]
        [SerializeField] private TileProperties[] propertiesData;
        
        public int MapId => mapId;
        public string MapName => mapName;
        public Vector2Int GridOriginCoordinate => gridOriginCoordinate;
        public Vector2Int GridSize => gridSize;
        public TileProperties[] PropertiesData => propertiesData;
        
        public bool IsPlantableAt(Vector3Int coordinate)
        {
            var properties = GetProperties(TilePropertyType.Plantable).Properties;
            return properties.Any(property => property.Coordinate == coordinate);
        }

        public bool IsNotDroppableAt(Vector3Int coordinate)
        {
            var properties = GetProperties(TilePropertyType.NotDroppable).Properties;
            return properties.Any(property => property.Coordinate == coordinate);
        }
        
        private TileProperties GetProperties(TilePropertyType propertyType) =>
            propertiesData[(int)propertyType];
    }
}