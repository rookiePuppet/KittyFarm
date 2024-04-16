using System.Linq;
using KittyFarm.Data;
using KittyFarm.Map;
using KittyFarm.UI;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace KittyFarm.Service
{
    public class TilemapService : MonoBehaviour, ITilemapService
    {
        [SerializeField] private TileBase dugTile;

        private MapDataSO propertiesData;
        private MapTilesDataSO tilesData;
        private Grid currentGrid;

        // public IEnumerable<Tilemap> Tilemaps => tilemapsOrganizer.Tilemaps;
        // public IEnumerable<Tuple<Tilemap, TilemapRenderer>> TilemapsWithRenderers =>
        //     tilemapsOrganizer.TilemapsWithRenderers;
        private TilemapsOrganizer tilemapsOrganizer;

        private Tilemap digTilemap;

        private void OnEnable()
        {
            SceneLoader.MapLoaded += Initialize;
        }

        private void OnDisable()
        {
            SceneLoader.MapLoaded -= Initialize;
        }

        private void Initialize(int mapId)
        {
            propertiesData = GameDataCenter.Instance.GetMapData(mapId);
            tilesData = GameDataCenter.Instance.GetMapTilesData(mapId);

            currentGrid = FindObjectOfType<Grid>();
            tilemapsOrganizer = currentGrid.GetComponent<TilemapsOrganizer>();
            tilemapsOrganizer.Initialize();

            digTilemap = tilemapsOrganizer.GetTilemap(TilemapSortingLayer.Dig);

            foreach (var item in tilesData.AllTilesDetails)
            {
                SetDugTileAt(item.CellPosition);
            }
        }

        public TilePropertiesInfo GetTilePropertiesInfoAt(Vector3Int cellPosition) =>
            new(IsPlantableAt(cellPosition), IsNotDroppableAt(cellPosition));

        public bool IsNotDroppableAt(Vector3Int cellPosition) => propertiesData.IsNotDroppableAt(cellPosition);

        #region Digging Methods

        public bool IsPlantableAt(Vector3Int cellPosition) => propertiesData.IsPlantableAt(cellPosition);

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

        private void SetDugTileAt(Vector3Int gridCoordinate)
        {
            digTilemap.SetTile(gridCoordinate, dugTile);
        }

        #endregion

        #region Tile Details Methods

        private bool TryGetTileDetailsOn(Vector3Int gridCoordinate, out TileDetails tileDetails)
        {
            foreach (var item in tilesData.AllTilesDetails.Where(item => item.CellPosition == gridCoordinate))
            {
                tileDetails = item;
                return true;
            }

            tileDetails = default;
            return false;
        }

        #endregion

        #region Grid Methods

        public Vector3Int WorldToCell(Vector3 worldPosition) => currentGrid.WorldToCell(worldPosition);

        public Vector3 GetCellCenterWorld(Vector3Int cellPosition) => currentGrid.GetCellCenterWorld(cellPosition);

        #endregion
    }
}