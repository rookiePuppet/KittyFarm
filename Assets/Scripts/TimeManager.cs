using System;
using Framework;
using UnityEngine;

namespace KittyFarm.Time
{
    public class TimeManager : MonoSingleton<TimeManager>
    {
        public static event Action SecondPassed;
        public static event Action MinutePassed;
        public static event Action HourPassed;
        public static event Action DayPassed;

        public static DateTime CurrentTime;

        private DateTime lastTime;

        private void Start()
        {
            CurrentTime = DateTime.Now;
            lastTime = CurrentTime;

            InvokeRepeating(nameof(CheckTime), 0, 1);
            InvokeRepeating(nameof(PlusOneMinute), 0, 1);
        }

        public void PlusOneMinute()
        {
            CurrentTime += TimeSpan.FromSeconds(1);
        }

        private void CheckTime()
        {
            if (CurrentTime.Second != lastTime.Second)
            {
                SecondPassed?.Invoke();

                if (CurrentTime.Minute != lastTime.Minute)
                {
                    MinutePassed?.Invoke();

                    if (CurrentTime.Hour != lastTime.Hour)
                    {
                        HourPassed?.Invoke();

                        if (CurrentTime.Day != lastTime.Day)
                        {
                            DayPassed?.Invoke();
                        }
                    }
                }
            }

            lastTime = CurrentTime;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CurrentTime += TimeSpan.FromHours(1);
            }
        }

        public static TimeSpan GetTimeSpanBetween(DateTime start, DateTime end)
        {
            return end - start;
        }

        public static TimeSpan GetTimeSpanFrom(DateTime beginTime)
        {
            return CurrentTime - beginTime;
        }
    }
}