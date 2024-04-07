using UnityEngine;

namespace KittyFarm
{
    public interface IUsableItem
    {
        public void Use(Vector3 worldPosition, Vector3Int cellPosition);
    }
}