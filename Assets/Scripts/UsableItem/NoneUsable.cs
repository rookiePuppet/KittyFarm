using UnityEngine;

namespace KittyFarm
{
    public class NoneUsable : IUsableItem
    {
        public NoneUsable()
        {
        }

        public void Use(Vector3 worldPosition, Vector3Int cellPosition)
        {
        }
    }
}