using DG.Tweening;
using KittyFarm.Data;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace KittyFarm.UI
{
    public class DialogBoard : UIBase, IPointerClickHandler
    {
        [SerializeField] private float typeContentDuration = 1f;
        [SerializeField] private TextMeshProUGUI contentText;
        [SerializeField] private GameObject clickToContinue;

        private readonly DialogController controller = new();

        public DialogContentDataSO ContentData
        {
            set => controller.Data = value;
        }

        private bool waitToContinue;

        public void BeginDialog()
        {
            ShowNext();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (waitToContinue)
            {
                ShowNext();
            }
        }

        private void ShowNext()
        {
            if (!controller.TryGetNext(out var content))
            {
                Hide();
                return;
            }

            TypeContent(content);
        }

        private void TypeContent(string content)
        {
            clickToContinue.SetActive(false);
            waitToContinue = false;
            contentText.text = string.Empty;
            
            DOTween.To(() => contentText.text, value => contentText.text = value, content, typeContentDuration)
                .onComplete += () =>
            {
                clickToContinue.SetActive(true);
                waitToContinue = true;
            };
        }
    }
}