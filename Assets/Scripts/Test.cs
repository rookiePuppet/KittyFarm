using System;
using KittyFarm.Time;
using UnityEngine;

public class Test : MonoBehaviour
{
    public int Hour = 6;
    public int Minute = 0;
    
    public bool ChangeInitialTime = false;

    private void Start()
    {
        if (ChangeInitialTime)
        {
            var today = DateTime.Today;
            TimeManager.CurrentTime = new DateTime(today.Year, today.Month, today.Day, Hour, Minute, 0);
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            TimeManager.Instance.AccelerateTime();
        }
    }
}