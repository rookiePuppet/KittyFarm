using System;
using KittyFarm.Time;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WorldLight : MonoBehaviour
{
    [SerializeField] private Gradient colorsInDay;

    [SerializeField] private int hour;
    [SerializeField] private int minute;
    [SerializeField] private int second;
    
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

    private float PercentOfDay =>
        (float)(TimeManager.CurrentTime - DateTime.Today).Ticks / TimeSpan.FromDays(1).Ticks;

    private void UpdateLightColor()
    {
        globalLight.color = colorsInDay.Evaluate(PercentOfDay);
    }
}
