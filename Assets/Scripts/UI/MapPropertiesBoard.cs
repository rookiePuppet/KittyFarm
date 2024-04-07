using UnityEngine;

namespace KittyFarm.UI
{
    public class MapPropertiesBoard : UIBase
    {
        [SerializeField] private TilePropertyEntry typeNameProperty;
        [SerializeField] private TilePropertyEntry tilePlantableProperty;
        [SerializeField] private TilePropertyEntry tileDroppableProperty;

        public void Refresh(TilePropertiesInfo info)
        {
            typeNameProperty.SetInfoText(info.TileTypeName);
            tilePlantableProperty.SetInfoBool(info.IsTilePlantable);
            tileDroppableProperty.SetInfoBool(info.IsTileDroppable);
        }
    }

    public struct TilePropertiesInfo
    {
        public string TileTypeName;
        public bool IsTilePlantable;
        public bool IsTileDroppable;

        public TilePropertiesInfo(string tileTypeName, bool isTilePlantable, bool isTileDroppable)
        {
            TileTypeName = tileTypeName;
            IsTilePlantable = isTilePlantable;
            IsTileDroppable = isTileDroppable;
        }
    }
}