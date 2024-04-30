using UnityEngine;

namespace KittyFarm.Service
{
    public class CameraService : MonoBehaviour, ICameraService
    {
        [SerializeField] private CameraController cameraController;

        private void Awake()
        {
            cameraController = FindObjectOfType<CameraController>();
        }

        public void EnableKineticCamera()
        {
            cameraController.EnableKineticCamera();
        }

        public void EnableFixedCamera()
        {
            cameraController.EnableFixedCamera();
        }

        public Vector3 ScreenToWorldPoint(Vector3 screenPosition) =>
            cameraController.ActiveCamera.ScreenToWorldPoint(screenPosition);
    }
}