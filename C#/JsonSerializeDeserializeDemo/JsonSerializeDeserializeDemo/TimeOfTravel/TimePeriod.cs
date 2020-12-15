using System;

namespace JsonSerializeDeserializeDemo.TimeOfTravel
{
    public class TimePeriod
    {
        public Int64 Id { get; }

        public SimpleTime Begin { get; }

        public SimpleTime End { get; }

        public TimePeriod(Int64 id, SimpleTime begin, SimpleTime end)
            => (Id, Begin, End) = (id, begin, end);

        #region Model identities

        public override int GetHashCode() => (int)Id;

        public override string ToString() => $"[{Id}]<Begin:{Begin},End:{End}>";

        #endregion

        public class SimpleTime : IComparable<TimeSpan>
        {
            public int Hour { get; }

            public int Minute { get; }

            public int Second { get; }

            public SimpleTime(int hour, int minute, int second)
                => (Hour, Minute, Second) = (hour, minute, second);

            public override string ToString() => $"<{Hour:00}:{Minute:00}:{Second:00}>";

            public int CompareTo(TimeSpan other)
            {
                if (Hour > other.Hours)
                {
                    return 1;
                }

                if (Hour < other.Hours)
                {
                    return -1;
                }

                if (Minute > other.Minutes)
                {
                    return 1;
                }

                if (Minute < other.Minutes)
                {
                    return -1;
                }

                if (Second > other.Seconds)
                {
                    return 1;
                }

                if (Second < other.Seconds)
                {
                    return -1;
                }

                return 0;
            }

            public static bool operator <(SimpleTime left, TimeSpan right)
            {
                return left.CompareTo(right) < 0;
            }

            public static bool operator <=(SimpleTime left, TimeSpan right)
            {
                return left.CompareTo(right) <= 0;
            }

            public static bool operator >(SimpleTime left, TimeSpan right)
            {
                return left.CompareTo(right) > 0;
            }

            public static bool operator >=(SimpleTime left, TimeSpan right)
            {
                return left.CompareTo(right) >= 0;
            }
        }
    }
}