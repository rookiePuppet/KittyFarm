using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KittyFarm.UI
{
    [ExecuteAlways]
    public class TilePropertyEntry : MonoBehaviour
    {
        public enum ValueType
        {
            TrueOrFalseSprite,
            SimpleText
        }

        [Header("Reference Components")]
        private Image circleImage;
        private TextMeshProUGUI nameText;
        private TextMeshProUGUI valueText;
        private Image valueImage;

        [SerializeField] private ObjectPropertyConfigurationSO configuration;

        [Header("Values")]
        public ValueType Type;
        public int CircleSpriteIndex;
        public string PropertyName;
        public string PropertyText;
        public bool PropertyBoolValue;

        private void Awake()
        {
            circleImage = transform.Find("CircleImage").GetComponent<Image>();
            nameText = transform.Find("NameText").GetComponent<TextMeshProUGUI>();
            valueText = transform.Find("ValueText").GetComponent<TextMeshProUGUI>();
            valueImage = transform.Find("ValueImage").GetComponent<Image>();
        }

        public void SetInfoBool(bool value)
        {
            valueImage.sprite = value ? configuration.TrueSprite : configuration.FalseSprite;
        }

        public void SetInfoText(string text)
        {
            valueText.text = text;
        }

        private void OnValidate()
        {
            if (configuration == null) return;

            circleImage ??= transform.Find("CircleImage").GetComponent<Image>();
            nameText ??= transform.Find("NameText").GetComponent<TextMeshProUGUI>();
            valueText ??= transform.Find("ValueText").GetComponent<TextMeshProUGUI>();
            valueImage ??= transform.Find("ValueImage").GetComponent<Image>();

            if (Type == ValueType.TrueOrFalseSprite)
            {
                valueImage.gameObject.SetActive(true);
                valueText.gameObject.SetActive(false);
            }
            else
            {
                valueText.gameObject.SetActive(true);
                valueImage.gameObject.SetActive(false);
            }

            circleImage.sprite = configuration.CircleSprites[CircleSpriteIndex];
            nameText.text = PropertyName;
            valueText.text = PropertyText;
            valueImage.sprite = PropertyBoolValue ? configuration.TrueSprite : configuration.FalseSprite;
        }
    }
}