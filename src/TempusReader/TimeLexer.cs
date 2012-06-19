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
        public const string InTimePattern = @"in";
        public const string FromNowTimePattern = @"from(\s+)now";
        public const string AgoTimePattern = @"ago";

        public TimeLexer() : base(Milliseconds, Seconds, Minutes, Hours, Days, InTime, FromNowTime, AgoTime, Separator, Whitespace, Number)
        {
        }

        public static readonly TokenKind Separator = new Pattern("separator", @",|(and)", skippable: true);
        public static readonly TokenKind Whitespace = new Pattern("whitespace", @"\s+", skippable: true);
        public static readonly TokenKind Number = new Pattern("number", @"(?=0(?!\d)|[1-9])\d+((\.|\:)\d+)?");

        public static readonly Pattern Milliseconds = new Pattern("milliseconds", MillisecondsPattern);
        public static readonly Pattern Seconds = new Pattern("seconds", SecondsPattern);
        public static readonly Pattern Minutes = new Pattern("minutes", MinutesPattern);
        public static readonly Pattern Hours = new Pattern("hours", HoursPattern);
        public static readonly Pattern Days = new Pattern("days", DaysPattern);
        public static readonly Pattern InTime = new Pattern("future", InTimePattern);
        public static readonly Pattern FromNowTime = new Pattern("future", FromNowTimePattern);
        public static readonly Pattern AgoTime = new Pattern("past", AgoTimePattern);
    }
}