using System;

namespace TempusReader
{
    public static class Extensions
    {
        public static Time InTime(this string @this)
        {
            return new Time(@this);
        }

        public static Time Minutes(this int minuteValue)
        {
            return new Time(TimeSpan.FromMinutes(minuteValue));
        }

        public static Time Seconds(this int secondValue)
        {
            return new Time(TimeSpan.FromSeconds(secondValue));
        }

        public static Time Days(this int dayValue)
        {
            return new Time(TimeSpan.FromDays(dayValue));
        }

        public static Time Hours(this int hourValue)
        {
            return new Time(TimeSpan.FromHours(hourValue));
        }

        public static Time Milliseconds(this int millisecondValue)
        {
            return new Time(TimeSpan.FromMilliseconds(millisecondValue));
        }
    }
}
