using DG.Tweening;
using UnityEngine;

namespace KittyFarm.InventorySystem
{
    public class FloatingEffect : MonoBehaviour
    {
        [SerializeField] private float floatingRange = 5f;
        [SerializeField] private float durationForOneTrip = 1f;

        private int direction; // 正向上，负向下

        private void Start()
        {
            var endPosY = transform.position.y - floatingRange / 2;
            transform.DOMoveY(endPosY, durationForOneTrip / 2).onComplete += () =>
            {
                Invoke(nameof(FloatingEase), 0f);
                direction = 1;
            };
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