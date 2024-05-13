using System.Collections.Generic;
using KittyFarm.Map;
using UnityEngine;

namespace KittyFarm.Data
{
    public class MapTilesDataSO : ScriptableObject
    {
        [SerializeField] private List<TileDetails> tilesDetailsList = new();
        
        public List<TileDetails> TilesDetailsList => tilesDetailsList;
        
        public const string PersistentDataName = "MapTilesData";
        
        public void AddTileDetails(TileDetails details)
        {
            TilesDetailsList.Add(details);
        }

        public void SetDigAsFalse(Vector3Int cellPosition)
        {
            var details = tilesDetailsList.Find(item => item.CellPosition == cellPosition);
            details.IsDug = false;
        }
    }
}