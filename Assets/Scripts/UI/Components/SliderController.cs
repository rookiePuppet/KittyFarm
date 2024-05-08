using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace KittyFarm.UI
{
    public class SliderController : MonoBehaviour, IDragHandler
    {
        public event Action<float> OnValueChanged;
        
        [SerializeField] private Image foregroundImage;

        [Space]
        [SerializeField] private int minification = 20;

        private float value;
        private Vector2 normalizedDelta;

        public float Value
        {
            get => value;
            set
            {
                this.value = value switch
                {
                    < 0 => 0,
                    > 1 => 1,
                    _ => value
                };
                
                foregroundImage.fillAmount = Value;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            normalizedDelta = eventData.delta.normalized;
            Value += normalizedDelta.x / minification;
            
            OnValueChanged?.Invoke(value);
        }
    }
}