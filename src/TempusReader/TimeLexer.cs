using Parsley;

namespace TempusReader
{
    internal class TimeLexer : Lexer
    {
        public const string DaysPattern = @"(days|day|d)";
        public const string HoursPattern = @"(hours|hour|hrs|hr)";
        public const string MinutesPattern = @"(minutes|minute|mins|min|m)";
        public const string SecondsPattern = @"(seconds|second|sec|s)";
        public const string MillisecondsPattern = @"(milliseconds|ms)";

        public TimeLexer() : base(Number, Milliseconds, Seconds, Minutes, Hours, Days, Separator, Whitespace)
        {
        }

        public static readonly TokenKind Separator = new Pattern("separator", @",|(and)", skippable: true);
        public static readonly TokenKind Whitespace = new Pattern("whitespace", @"\s+", skippable: true);

        public static readonly Pattern Number = new Pattern("number", @"[0-9]+");

        public static readonly Pattern Milliseconds = new Pattern("milliseconds", MillisecondsPattern);
        public static readonly Pattern Seconds = new Pattern("seconds", SecondsPattern);
        public static readonly Pattern Minutes = new Pattern("minutes", MinutesPattern);
        public static readonly Pattern Hours = new Pattern("hours", HoursPattern);
        public static readonly Pattern Days = new Pattern("days", DaysPattern);
    }
}