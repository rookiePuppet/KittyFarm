using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace KittyFarm.Map
{
    [ExecuteInEditMode]
    public class TilemapGridPropertiesHolder : MonoBehaviour
    {
        [SerializeField] private List<TilemapWithTilePropertyType> content;
        
        public Dictionary<TilePropertyType, Tilemap> PropertyTilemaps { get; private set; } = new();
        
        private void OnDisable()
        {
            if (Application.isPlaying) return;
        
            LoadGridProperties();
        }

        /// <summary>
        /// 加载所有子物体上的瓦片地图和属性信息
        /// </summary>
        public void LoadGridProperties()
        {
            var tilemaps = GetComponentsInChildren<Tilemap>();
            content = new List<TilemapWithTilePropertyType>(tilemaps.Length);
            PropertyTilemaps.Clear();
            foreach (var tilemap in tilemaps)
            {
                var type = GetPropertyType(tilemap.gameObject.layer);
                content.Add(new TilemapWithTilePropertyType
                {
                    Tilemap = tilemap,
                    PropertyType = type
                });

                PropertyTilemaps.Add(type, tilemap);
            }
        }

        private static TilePropertyType GetPropertyType(int layer)
        {
            var layerName = LayerMask.LayerToName(layer);
            return Enum.TryParse<TilePropertyType>(layerName, out var propertyType)
                ? propertyType
                : TilePropertyType.Obstacle;
        }
    }
}