using KittyFarm.UI;
using TMPro;
using UnityEngine;

public class DebugView : UIBase
{
    public TextMeshProUGUI frameRateText;

    private int frameRate;

    private void Start()
    {
        InvokeRepeating(nameof(UpdateFrameRate),0, 1f);
    }

    private void UpdateFrameRate()
    {
        frameRate = Mathf.RoundToInt(1f / Time.deltaTime);
        frameRateText.text = $"FPS  {frameRate}";
    }
}