using KittyFarm.CropSystem;
using UnityEngine;

namespace KittyFarm.Map
{
    [CreateAssetMenu(fileName = "MapData", menuName = "Kitty Farm/Map Data")]
    public class MapDataSO : ScriptableObject
    {
        [Header("基础信息")]
        [SerializeField] private string mapName = "Default";
        [SerializeField] private Vector2Int gridOriginCoordinate;
        [SerializeField] private Vector2Int gridSize;

        [Header("地图网格属性数据")]
        [SerializeField] private MapPropertiesDataSO propertiesData;

        [Header("地图上作物数据")]
        [SerializeField] private MapCropsDataSO cropsData;

        [Header("地图瓦片数据")]
        [SerializeField] private MapTilesDataSO tilesData;
        
        public string MapName => mapName;
        public Vector2Int GridOriginCoordinate => gridOriginCoordinate;
        public Vector2Int GridSize => gridSize;
        
        public MapPropertiesDataSO PropertiesData => propertiesData;
        public MapCropsDataSO CropsData => cropsData;
        public MapTilesDataSO TilesData => tilesData;
    }
}