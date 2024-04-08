using System;

namespace KittyFarm.Map
{
    public class SortingLayerNameTransformer
    {
        public static string GetSortingLayerName(TilemapSortingLayer layer) => layer switch
        {
            TilemapSortingLayer.Water => "水",
            TilemapSortingLayer.Ground => "地基",
            TilemapSortingLayer.Dirt => "耕地",
            TilemapSortingLayer.GrassLayerOne => "草层1",
            TilemapSortingLayer.GrassLayerTwo => "草层2",
            _ => "未知"
        };
    }
}