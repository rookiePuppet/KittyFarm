using System;
using System.Collections.Generic;
using System.Linq;
using KittyFarm.Map;
using KittyFarm.UI;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace KittyFarm.Service
{
    public class MapService : MonoBehaviour, IMapService
    {
        [SerializeField] private TileBase dugTile;

        public MapDataSO MapData { get; private set; }
        public IEnumerable<Tilemap> Tilemaps => tilemapsOrganizer.Tilemaps;
        public IEnumerable<Tuple<Tilemap, TilemapRenderer>> TilemapsWithRenderers =>
            tilemapsOrganizer.TilemapsWithRenderers;

        private TilemapsOrganizer tilemapsOrganizer;
        private Grid currentGrid;

        public void Initialize(string mapName)
        {
            MapData = Resources.Load<MapDataSO>($"MapData/MapData{{{mapName}}}");

            currentGrid = FindObjectOfType<Grid>();
            tilemapsOrganizer = FindObjectOfType<TilemapsOrganizer>();

            tilemapsOrganizer.Initialize(currentGrid.transform);

            foreach (var item in MapData.TilesData.TileDetailsData)
            {
                SetDugTileAt(item.CellPosition);
            }
        }

        public TilePropertiesInfo GetTilePropertiesInfoAt(Vector3Int cellPosition) =>
            new TilePropertiesInfo(IsPlantableAt(cellPosition), IsNotDroppableAt(cellPosition));

        public bool IsNotDroppableAt(Vector3Int cellPosition) => MapData.PropertiesData.IsNotDroppable(cellPosition);

        #region Digging Methods

        public bool IsPlantableAt(Vector3Int cellPosition) => MapData.PropertiesData.IsPlantable(cellPosition);

        public bool CheckWasDugAt(Vector3Int cellPosition) =>
            TryGetTileDetailsOn(cellPosition, out var tileDetails) && tileDetails.IsDug;

        public void DigAt(Vector3Int cellPosition)
        {
            SetDugTileAt(cellPosition);

            SaveTileDetails(new TileDetails
            {
                CellPosition = cellPosition,
                IsDug = true,
                LastDugTime = DateTime.Now
            });
        }

        private void SetDugTileAt(Vector3Int gridCoordinate)
        {
            var dirt = tilemapsOrganizer.GetTilemap(TilemapSortingLayer.Dig);
            dirt.SetTile(gridCoordinate, dugTile);
        }

        #endregion

        #region Tile Details Methods

        private bool TryGetTileDetailsOn(Vector3Int gridCoordinate, out TileDetails tileDetails)
        {
            foreach (var item in MapData.TilesData.TileDetailsData.Where(item => item.CellPosition == gridCoordinate))
            {
                tileDetails = item;
                return true;
            }

            tileDetails = default;
            return false;
        }

        private void SaveTileDetails(TileDetails tileDetails)
        {
            MapData.TilesData.SaveTileDetails(tileDetails);
        }

        #endregion

        #region Grid Methods

        public Vector3Int WorldToCell(Vector3 worldPosition) => currentGrid.WorldToCell(worldPosition);

        public Vector3 GetCellCenterWorld(Vector3Int cellPosition) => currentGrid.GetCellCenterWorld(cellPosition);

        #endregion
    }
}