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
            if (TryGetTileDetails(details.CellPosition, out var existDetails))
            {
                return;
            } 
            TilesDetailsList.Add(details);
        }

        public void SetDigAsTrue(Vector3Int cellPosition)
        {
            if (!TryGetTileDetails(cellPosition, out var details))
            {
                details = new TileDetails
                {
                    CellPosition =  cellPosition,
                };
                
                tilesDetailsList.Add(details);
            }
            
            details.IsDug = true;
        }

        public void SetDigAsFalse(Vector3Int cellPosition)
        {
            if (!TryGetTileDetails(cellPosition, out var details))
            {
                details = new TileDetails
                {
                    CellPosition =  cellPosition
                };
                
                tilesDetailsList.Add(details);
            }
            
            details.IsDug = false;
        }

        private bool TryGetTileDetails(Vector3Int cellPosition, out TileDetails details)
        {
            details = tilesDetailsList.Find(item => item.CellPosition == cellPosition);
            return details != null;
        }
    }
}