using System.Collections.Generic;
using UnityEngine;

namespace KittyFarm.Map
{
    public class MapTilesDataSO : ScriptableObject
    {
        public List<TileDetails> TileDetailsData = new();

        public void SaveTileDetails(TileDetails details)
        {
            TileDetailsData.Add(details);
        }
    }
}