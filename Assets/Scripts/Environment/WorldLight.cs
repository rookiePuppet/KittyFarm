using System;
using KittyFarm.Time;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WorldLight : MonoBehaviour
{
    [SerializeField] private Gradient colorsInDay;
    
    private Light2D globalLight;

    private void Awake()
    {
        globalLight = GetComponent<Light2D>();
        globalLight.enabled = false;
    }

    private void OnEnable()
    {
        TimeManager.SecondPassed += UpdateLightColor;
    }

    private void OnDisable()
    {
        TimeManager.SecondPassed -= UpdateLightColor;
    }

    private void Start()
    {
        UpdateLightColor();
        globalLight.enabled = true;
    }

    // private float PercentOfDay =>
    //     (float)(TimeManager.CurrentTime - DateTime.Today).Ticks / TimeSpan.FromDays(1).Ticks;

    // 测试版本中可能会加速时间，导致该比值可能始终等于1，光照颜色不能一直循环，所以采用以下方式计算
    private float PercentOfDay
    {
        get
        {
            var oneDay = TimeSpan.FromDays(1);
            var elapsedTime = TimeManager.CurrentTime - DateTime.Today;
            while (elapsedTime.Ticks > oneDay.Ticks)
            {
                elapsedTime -= oneDay;
            }

            return (float)elapsedTime.Ticks / oneDay.Ticks;
        }
    }
    
    private void UpdateLightColor()
    {
        globalLight.color = colorsInDay.Evaluate(PercentOfDay);
    }
}
