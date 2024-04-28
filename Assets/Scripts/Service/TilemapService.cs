using System;
using KittyFarm.Data;
using KittyFarm.Map;
using KittyFarm.Time;
using KittyFarm.UI;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace KittyFarm.Service
{
    public class TilemapService : MonoBehaviour, ITilemapService
    {
        // [Tooltip("地面湿润值变为0的总时间（分钟）")] [SerializeField] private int dryTime = 60;

        [SerializeField] private TileBase dugTile;

        public Grid CurrentGrid => currentGrid;

        private MapDataSO mapData;
        private MapTilesDataSO tilesData;
        private Grid currentGrid;

        private TilemapsOrganizer tilemapsOrganizer;
        private Tilemap digTilemap;

        private void OnEnable()
        {
            SceneLoader.MapLoaded += Initialize;
            // TimeManager.MinutePassed += UpdateAllTileDetails;
        }

        private void OnDisable()
        {
            SceneLoader.MapLoaded -= Initialize;
            // TimeManager.MinutePassed -= UpdateAllTileDetails;
        }

        private void Initialize()
        {
            mapData = GameDataCenter.Instance.MapData;
            tilesData = GameDataCenter.Instance.MapTilesData;

            currentGrid = FindObjectOfType<Grid>();
            tilemapsOrganizer = currentGrid.GetComponent<TilemapsOrganizer>();
            tilemapsOrganizer.Initialize();

            digTilemap = tilemapsOrganizer.GetTilemap(TilemapSortingLayer.Dig);

            foreach (var item in tilesData.TilesDetailsList)
            {
                SetDugTileAt(item.CellPosition);
            }

            // UpdateAllTileDetails();
        }
        
        // private void UpdateAllTileDetails()
        // {
        //     foreach (var tileDetails in tilesData.TilesDetailsList)
        //     {
        //         UpdateSingleTileDetails(tileDetails);
        //     }
        // }
        //
        // private void UpdateSingleTileDetails(TileDetails tileDetails)
        // {
        //     var dryValue = (float)TimeManager.GetTimeSpanFrom(tileDetails.LastWateringTime).Ticks /
        //                    TimeSpan.FromMinutes(dryTime).Ticks;
        //     tileDetails.WettingValue = 1 - Mathf.Min(dryValue, 1);
        // }

        public TilePropertiesInfo GetTilePropertiesInfoAt(Vector3Int cellPosition) =>
            new(IsPlantableAt(cellPosition), IsNotDroppableAt(cellPosition));

        public bool IsNotDroppableAt(Vector3Int cellPosition) => mapData.IsNotDroppableAt(cellPosition);

        #region Digging Methods

        public bool IsPlantableAt(Vector3Int cellPosition) => mapData.IsPlantableAt(cellPosition);

        public bool CheckWasDugAt(Vector3Int cellPosition) =>
            TryGetTileDetailsOn(cellPosition, out var tileDetails) && tileDetails.IsDug;

        public void DigAt(Vector3Int cellPosition)
        {
            SetDugTileAt(cellPosition);

            tilesData.AddTileDetails(new TileDetails
            {
                CellPosition = cellPosition,
                IsDug = true
            });
        }

        // public void WaterAt(Vector3Int cellPosition)
        // {
        //     if (TryGetTileDetailsOn(cellPosition, out var tileDetails))
        //     {
        //         tileDetails.LastWateringTime = TimeManager.CurrentTime;
        //         UpdateSingleTileDetails(tileDetails);
        //     }
        // }

        private void SetDugTileAt(Vector3Int gridCoordinate)
        {
            digTilemap.SetTile(gridCoordinate, dugTile);
        }

        #endregion

        #region Tile Details Methods

        public bool TryGetTileDetailsOn(Vector3Int gridCoordinate, out TileDetails tileDetails)
        {
            tileDetails = null;
            foreach (var item in tilesData.TilesDetailsList)
            {
                if (item.CellPosition == gridCoordinate)
                {
                    tileDetails = item;
                }
            }

            return tileDetails != null;
        }

        #endregion

        #region Grid Methods

        public Vector3Int WorldToCell(Vector3 worldPosition) => currentGrid.WorldToCell(worldPosition);

        public Vector3 GetCellCenterWorld(Vector3Int cellPosition) => currentGrid.GetCellCenterWorld(cellPosition);

        #endregion
    }
}