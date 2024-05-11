using KittyFarm.Time;
using TMPro;
using UnityEngine;

namespace KittyFarm.UI
{
    public class DebugView : UIBase
    {
        [SerializeField] private GameObject testLayout;
        [SerializeField] private TextMeshProUGUI frameRateText;
    
        private int frameRate;
        private bool showTestLayout;

        private bool isAccelerateTimeButtonPressed;

        private void Start()
        {
            InvokeRepeating(nameof(UpdateFrameRate),0, 1f);
        }
    
        private void Update()
        {
            frameRate = Mathf.RoundToInt(1f / UnityEngine.Time.deltaTime);

            if (isAccelerateTimeButtonPressed)
            {
                AccelerateTime();
            }
        }

        private void UpdateFrameRate()
        {
            frameRateText.text = $"FPS  {frameRate}";
        }

        public void InvertTestLayerVisible()
        {
            showTestLayout = !showTestLayout;
            testLayout.SetActive(showTestLayout);
        }

        public void OnAccelerateTimeButtonDown()
        {
            isAccelerateTimeButtonPressed = true;
        }
    
        public void OnAccelerateTimeButtonUp()
        {
            isAccelerateTimeButtonPressed = false;
        }

        private void AccelerateTime()
        {
            TimeManager.Instance.AccelerateTime();
        }

        public void ResetTime()
        {
            TimeManager.Instance.ResetTime();
        }
    }
}