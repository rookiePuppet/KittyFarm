using System;
using UnityEngine;

namespace KittyFarm.Service
{
    public class CameraService : MonoBehaviour, ICameraService
    {
        private Camera currentCamera;

        private void OnEnable()
        {
            GameManager.MapChanged += Initialize;
        }

        private void Initialize()
        {
            currentCamera = Camera.main;
        }

        public Vector3 ScreenToWorldPoint(Vector3 screenPosition) =>
            currentCamera.ScreenToWorldPoint(screenPosition);

        public Vector3 WorldToScreenPoint(Vector3 worldPosition) =>
            currentCamera.WorldToScreenPoint(worldPosition);
    }
}