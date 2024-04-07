using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace KittyFarm.Map
{
    public class TilemapsOrganizer : MonoBehaviour
    {
        public IEnumerable<Tilemap> Tilemaps => tilemaps;
        public IEnumerable<Tuple<Tilemap, TilemapRenderer>> TilemapsWithRenderers => tilemaps.Select((tilemap, index) =>
            new Tuple<Tilemap, TilemapRenderer>(tilemap, tilemapRenderers[index]));

        private readonly List<Tilemap> tilemaps = new();
        private readonly List<TilemapRenderer> tilemapRenderers = new();

        public void Initialize(Transform tilemapsParent)
        {
            tilemaps.Clear();
            tilemapRenderers.Clear();

            foreach (Transform child in tilemapsParent)
            {
                if (child.gameObject.layer != LayerMask.NameToLayer("Tilemap")) continue;
                tilemapRenderers.Add(child.GetComponent<TilemapRenderer>());
            }

            tilemapRenderers.Sort((left, right) =>
            {
                var sortingLeft = SortingLayer.GetLayerValueFromID(left.sortingLayerID);
                var sortingRight = SortingLayer.GetLayerValueFromID(right.sortingLayerID);
                return sortingLeft > sortingRight ? -1 : 1;
            });

            foreach (var render in tilemapRenderers)
            {
                var tilemap = render.gameObject.GetComponent<Tilemap>();
                tilemaps.Add(tilemap);
            }
        }

        public TilemapRenderer GetTilemapRenderer(TilemapSortingLayer layer)
        {
            return tilemapRenderers.Find(tilemap => tilemap.sortingLayerName == layer.ToString());
        }

        public Tilemap GetTilemap(TilemapSortingLayer layer)
        {
            var index = tilemapRenderers.FindIndex(tilemap => tilemap.sortingLayerName == layer.ToString());
            return tilemaps[index];
        }
    }
}