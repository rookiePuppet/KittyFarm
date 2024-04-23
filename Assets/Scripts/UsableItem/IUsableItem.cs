using UnityEngine;

namespace KittyFarm
{
    public interface IUsableItem
    {
        public bool CanUse { get; }
        public void Use();
    }
}