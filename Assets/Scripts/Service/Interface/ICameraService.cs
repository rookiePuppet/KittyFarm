using UnityEngine;

namespace KittyFarm.Service
{
    public interface ICameraService: IService
    {
        public Vector3 ScreenToWorldPoint(Vector3 screenPosition);
        public Vector3 WorldToScreenPoint(Vector3 worldPosition);
    }
}