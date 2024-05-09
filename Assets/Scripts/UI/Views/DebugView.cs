using KittyFarm.UI;
using TMPro;
using UnityEngine;

public class DebugView : UIBase
{
    [SerializeField] private TextMeshProUGUI frameRateText;

    private int frameRate;

    private void Start()
    {
        InvokeRepeating(nameof(UpdateFrameRate),0, 1f);
    }

    private void Update()
    {
        frameRate = Mathf.RoundToInt(1f / Time.deltaTime);
    }

    private void UpdateFrameRate()
    {
        frameRateText.text = $"FPS  {frameRate}";
    }
}