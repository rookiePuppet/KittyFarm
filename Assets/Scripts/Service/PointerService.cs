using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

namespace KittyFarm.Service
{
    public class PointerService : MonoBehaviour, IPointerService
    {
        public Camera MainCamera { get; private set; }

        private void Awake()
        {
            MainCamera = Camera.main;
        }

        private void OnEnable()
        {
            TouchSimulation.Enable();
        }

        public Vector3 ScreenToWorldPoint(Vector3 screenPosition) => MainCamera.ScreenToWorldPoint(screenPosition);
    }
}