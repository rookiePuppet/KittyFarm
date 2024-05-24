using DG.Tweening;
using UnityEngine;

namespace KittyFarm.InventorySystem
{
    public class FloatingEffect : MonoBehaviour
    {
        [SerializeField] private bool isEnabled = true;
        [SerializeField] private float floatingRange = 2.5f;
        [SerializeField] private float durationForOneTrip = 1f;

        private int direction = 1; // 正向上，负向下

        private void Start()
        {
            FloatingEase();
        }

        private void FloatingEase()
        {
            var endPosY = transform.position.y + floatingRange * direction;
            transform.DOMoveY(endPosY, durationForOneTrip).onComplete += () =>
            {
                direction = -direction;
                FloatingEase();
            };
        }
    }
}