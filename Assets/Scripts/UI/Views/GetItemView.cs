using DG.Tweening;
using KittyFarm.Data;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace KittyFarm.UI
{
    public class GetItemView : UIBase, IPointerClickHandler
    {
        [SerializeField] private float popupDuration = 0.5f;
        [SerializeField] private Image iconImage;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI amountText;

        [SerializeField] private RectTransform content;
        
        private Image backMask;
        private Color backMaskColor;
        private bool closable;

        private void Awake()
        {
            backMask = GetComponent<Image>();
            backMaskColor = backMask.color;
        }

        public void Initialize(ItemDataSO itemData, int amount)
        {
            iconImage.sprite = itemData.IconSprite;
            nameText.text = itemData.ItemName;
            amountText.text = $"X{amount}";
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!closable) return;
            Hide();
        }

        public override void Show()
        {
            content.localScale = Vector3.zero;
            backMask.color = new Color(0, 0, 0, 0);
            base.Show();

            backMask.DOColor(backMaskColor, popupDuration);
            content.DOScale(Vector3.one, popupDuration).onComplete += () => { closable = true; };
        }

        public override void Hide()
        {
            backMask.DOColor(new Color(0, 0, 0, 0), popupDuration);
            content.DOScale(Vector3.zero, popupDuration).onComplete += () => { base.Hide(); };
            closable = false;
        }
    }
}