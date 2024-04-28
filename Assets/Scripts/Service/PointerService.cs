using UnityEngine;

namespace KittyFarm.Service
{
    public class PointerService : MonoBehaviour, IPointerService
    {
        public Camera MainCamera { get; private set; }

        private void Awake()
        {
            MainCamera = Camera.main;
        }

        public Vector3 ScreenToWorldPoint(Vector3 screenPosition) => MainCamera.ScreenToWorldPoint(screenPosition);
    }
}