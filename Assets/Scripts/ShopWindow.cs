using KittyFarm.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopWindow : UIBase
{
    [SerializeField] private Button closeButton;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private Image commodityImage;
    [SerializeField] private TextMeshProUGUI commodityNameText;
    [SerializeField] private TextMeshProUGUI commodityDescriptionText;
    [SerializeField] private TextMeshProUGUI commodityPriceText;
    [SerializeField] private CounterController counter;
    [SerializeField] private Button purchaseButton;

    private void Awake()
    {
        closeButton.onClick.AddListener(Hide);
    }
}