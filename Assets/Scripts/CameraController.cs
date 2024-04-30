using UnityEngine;

namespace KittyFarm
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Camera kineticCamera;
        [SerializeField] private Camera fixedCamera;

        public Camera ActiveCamera { get; private set; }

        public void EnableKineticCamera()
        {
            kineticCamera.enabled = true;
            fixedCamera.enabled = false;

            ActiveCamera = kineticCamera;
        }

        public void EnableFixedCamera()
        {
            kineticCamera.enabled = false;
            fixedCamera.enabled = true;

            ActiveCamera = fixedCamera;
        }
    }
}