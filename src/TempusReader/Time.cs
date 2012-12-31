using System;
using System.Collections.Generic;
using System.Linq;
using Parsley;

namespace TempusReader
{
    public class Time : IEquatable<Time>, IComparable<Time>
    {
        private static readonly Lexer Lexer = new TimeLexer();
        private static readonly IDictionary<string, Func<double, TimeSpan>> DateParts = new Dictionary<string, Func<double, TimeSpan>>
        {
            { TimeLexer.Milliseconds.Name, TimeSpan.FromMilliseconds },
            { TimeLexer.Seconds.Name, TimeSpan.FromSeconds },
            { TimeLexer.Minutes.Name, TimeSpan.FromMinutes },
            { TimeLexer.Hours.Name, TimeSpan.FromHours },
            { TimeLexer.Days.Name, TimeSpan.FromDays },
        };

        private readonly TimeSpan _timeSpan;

        public Time(TimeSpan timeSpan)
        {
            _timeSpan = timeSpan;
        }

        public Time(string input)
        {
            var tokens = Lexer.Tokenize(input).ToArray();

            _timeSpan = TempusGrammar.TimeSpan.Parse(new TokenStream(tokens)).Value;
        }

        public static implicit operator TimeSpan(Time time)
        {
            return time == null ? default(TimeSpan) : time._timeSpan;
        }

        public int CompareTo(Time other)
        {
            return _timeSpan.CompareTo(other._timeSpan);
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

        public static TimeSpan FromDateParts(IEnumerable<KeyValuePair<string, double>> times)
        {
            return times.Aggregate(new TimeSpan(), (current, t) => current.Add(DateParts[t.Key](t.Value)));
        }
    }
}