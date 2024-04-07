using System;
using Framework;

namespace KittyFarm.Time
{
    public class TimeManager : MonoSingleton<TimeManager>
    {
        public event Action SecondPassed;
        public event Action MinutePassed;
        public event Action HourPassed;
        public event Action DayPassed;

        public DateTime CurrentTime => DateTime.Now;

        private DateTime lastTime;

        private void Start()
        {
            lastTime = CurrentTime;

            InvokeRepeating(nameof(CheckTime), 0, 1);
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

        public static TimeSpan GetTimeSpanBetween(DateTime start, DateTime end)
        {
            return end - start;
        }

        public static TimeSpan GetTimeSpanFrom(DateTime beginTime)
        {
            return DateTime.Now - beginTime;
        }
    }
}