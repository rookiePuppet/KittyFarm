using System;
using Framework;

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

        static TimeManager()
        {
            CurrentTime = DateTime.Now;
        }

        private void Start()
        {
            lastTime = CurrentTime;
            InvokeRepeating(nameof(CheckTime), 0, 1);
            InvokeRepeating(nameof(PlusOneSecond), 0, 1);
        }

        public void PlusOneSecond()
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

        public void AccelerateTime()
        {
            CurrentTime += TimeSpan.FromMinutes(1);
            MinutePassed?.Invoke();
        }

        public void ResetTime()
        {
            CurrentTime = DateTime.Now;
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