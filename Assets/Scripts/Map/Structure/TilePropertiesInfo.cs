namespace KittyFarm.UI
{
    public struct TilePropertiesInfo
    {
        public bool IsTilePlantable;
        public bool IsTileDroppable;

        public TilePropertiesInfo(bool isTilePlantable, bool isTileDroppable)
        {
            IsTilePlantable = isTilePlantable;
            IsTileDroppable = isTileDroppable;
        }
    }
}