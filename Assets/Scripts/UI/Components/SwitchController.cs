using System;
using UnityEngine;
using UnityEngine.UI;

namespace KittyFarm.UI
{
    public class SwitchController : MonoBehaviour
    {
        public event Action<bool> OnSwitch;
        
        [SerializeField] private Sprite offBackground;
        [SerializeField] private Sprite onBackground;
        [Space]
        [SerializeField] private RectTransform handle;
        
        public bool IsOn
        {
            get => isOn;
            set
            {
                isOn = value;
                backgroundImage.sprite = value ? onBackground : offBackground;
                handle.anchoredPosition = value ? handleOnPos : handleOffPos;
            }
        }
        
        private Vector2 handleOffPos;
        private Vector2 handleOnPos;
        
        private Image backgroundImage;
        private Button button;
        
        private bool isOn;
        
        private void Awake()
        {
            backgroundImage = GetComponent<Image>();
            button = GetComponent<Button>();

            handleOffPos = Vector2.zero;
            handleOnPos = new Vector2(((RectTransform)transform).rect.width, 0);

            backgroundImage.sprite = onBackground;
            handle.anchoredPosition = handleOnPos;
        }

        private void OnEnable()
        {
            button.onClick.AddListener(Switch);
        }
        
        private void OnDisable()
        {
            button.onClick.RemoveListener(Switch);
        }

        private void Switch()
        {
            IsOn = !IsOn;
            OnSwitch?.Invoke(IsOn);
        }
    }
}