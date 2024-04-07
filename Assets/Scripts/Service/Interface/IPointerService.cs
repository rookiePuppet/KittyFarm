using UnityEngine;

namespace KittyFarm.Service
{
    public interface IPointerService: IService
    {
        public Camera MainCamera { get; }
        public Vector3 ScreenToWorldPoint(Vector3 screenPosition);
    }
}