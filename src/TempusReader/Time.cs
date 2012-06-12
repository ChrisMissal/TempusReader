using System;
using System.Collections.Generic;
using System.Linq;
using Parsley;

namespace TempusReader
{
    public class Time : IEquatable<Time>, IComparable<Time>
    {
        private static readonly IDictionary<string, Func<double, TimeSpan>> DateParts = new Dictionary<string, Func<double, TimeSpan>>
        {
            { "milliseconds", TimeSpan.FromMilliseconds },
            { "seconds", TimeSpan.FromSeconds },
            { "minutes", TimeSpan.FromMinutes },
            { "hours", TimeSpan.FromHours },
            { "days", TimeSpan.FromDays },
        };

        private readonly TimeSpan _timeSpan;

        public Time(TimeSpan timeSpan)
        {
            _timeSpan = timeSpan;
        }

        public Time(string input)
        {
            var lexer = new TimeLexer();
            var tokens = lexer.Tokenize(input).ToArray();

            _timeSpan = TimeGrammar.TimeSpan.Parse(new TokenStream(tokens)).Value;
        }

        public static implicit operator TimeSpan(Time time)
        {
            return time == null ? default(TimeSpan) : time._timeSpan;
        }

        public int CompareTo(Time other)
        {
            return _timeSpan.CompareTo(other);
        }

        public override string ToString()
        {
            return _timeSpan.ToString();
        }

        public bool Equals(Time other)
        {
            return _timeSpan.Equals(other._timeSpan);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Time)) return false;
            return Equals((Time)obj);
        }

        public override int GetHashCode()
        {
            return _timeSpan.GetHashCode();
        }

        public static bool operator ==(Time left, Time right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Time left, Time right)
        {
            return !Equals(left, right);
        }

        public static TimeSpan FromDatePart(string datePart, double amount)
        {
            return DateParts[datePart](amount);
        }
    }
}