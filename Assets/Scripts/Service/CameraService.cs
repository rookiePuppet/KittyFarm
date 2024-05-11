using UnityEngine;

namespace KittyFarm.Service
{
    public class CameraService : MonoBehaviour, ICameraService
    {
        private Camera CurrentCamera
        {
            get
            {
                if (currentCamera == null)
                {
                    currentCamera = Camera.main;
                }

                return currentCamera;
            }
        }
        private Camera currentCamera;
        
        public Vector3 ScreenToWorldPoint(Vector3 screenPosition) =>
            CurrentCamera.ScreenToWorldPoint(screenPosition);

        public Vector3 WorldToScreenPoint(Vector3 worldPosition) =>
            CurrentCamera.WorldToScreenPoint(worldPosition);
    }
}