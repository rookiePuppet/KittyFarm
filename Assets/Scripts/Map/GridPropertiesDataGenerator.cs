using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace KittyFarm.Map
{
    public class GridPropertiesDataGenerator : MonoBehaviour
    {
        [SerializeField] private MapDataSO mapData;
        [SerializeField] private TilemapGridPropertiesHolder propertiesHolder;

        private MapPropertiesDataSO PropertiesData => mapData.PropertiesData;

        public void GenerateData()
        {
            if (Application.isPlaying) return;

            if (propertiesHolder.PropertyTilemaps.Count == 0)
            {
                propertiesHolder.LoadGridProperties();
            }

            PropertiesData.Data =
                new TileProperties[Enum.GetValues(typeof(TilePropertyType)).Length];

            foreach (var (type, tilemap) in propertiesHolder.PropertyTilemaps)
            {
                tilemap.CompressBounds();

                var properties = new List<TileProperty>();
                foreach (var position in tilemap.cellBounds.allPositionsWithin)
                {
                    if (tilemap.GetTile(position) is null) continue;

                    var newProperty = new TileProperty
                    {
                        Coordinate = position,
                        Type = type,
                    };

                    properties.Add(newProperty);
                }

                PropertiesData.Data[(int)type] = new TileProperties
                {
                    PropertyType = type,
                    Properties = properties
                };
            }

#if UNITY_EDITOR
            EditorUtility.SetDirty(mapData);
#endif

            Debug.Log($"地图{mapData.MapName}: 瓦片属性数据已更新");
        }
    }
}