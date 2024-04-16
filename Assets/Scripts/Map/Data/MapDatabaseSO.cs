using System.Collections.Generic;
using UnityEngine;

namespace KittyFarm.Map
{
    [CreateAssetMenu(fileName = "MapDatabase", menuName = "Map/Map Database")]
    public class MapDatabaseSO : ScriptableObject
    {
        [field: SerializeField] private List<MapDataSO> MapDataList { get; set; }
        
        public MapDataSO GetMapData(int id) => MapDataList.Find(mapData => mapData.MapId == id);
    }
}