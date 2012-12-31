using System;
using System.Linq;
using Parsley;

namespace TempusReader
{
    public class Date : IEquatable<Date>, IComparable<Date>
    {
        private static readonly Lexer Lexer = new TimeLexer();
        private DateTime _dateTime;

        public Date(DateTime dateTime)
        {
            _dateTime = dateTime;
        }

        public Date(DateTime dateTime, string input)
        {
            var tokens = Lexer.Tokenize(input).ToArray();

            var difference = TempusGrammar.Time.Parse(new TokenStream(tokens)).Value;
            _dateTime = dateTime.Add(difference);
        }

        public static implicit operator DateTime?(Date date)
        {
            return date == null ? default(DateTime?) : date._dateTime;
        }

        public static implicit operator DateTime(Date date)
        {
            return date == null ? default(DateTime) : date._dateTime;
        }

        public bool Equals(Date other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other._dateTime.Equals(_dateTime);
        }

        public int CompareTo(Date other)
        {
            return _dateTime.CompareTo(other._dateTime);
        }

        public override string ToString()
        {
            return _dateTime.ToString();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Date)) return false;
            return Equals((Date)obj);
        }

        public override int GetHashCode()
        {
            return _dateTime.GetHashCode();
        }

        public static bool operator ==(Date left, Date right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Date left, Date right)
        {
            return !Equals(left, right);
        }
    }
}