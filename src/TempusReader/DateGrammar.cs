using Parsley;

namespace TempusReader
{
    internal class DateGrammar : Grammar
    {
        private static readonly GrammarRule<Time> TimePrefix = new GrammarRule<Time>();
        private static readonly GrammarRule<Time> TimeSuffix = new GrammarRule<Time>();
        public static readonly GrammarRule<Time> Time = new GrammarRule<Time>();

        static DateGrammar()
        {
            TimePrefix.Rule = from prefix in Choice(Token(TimeLexer.InTime))
                              from timeSpan in TimeGrammar.TimeSpan
                              select new Time(timeSpan);

            TimeSuffix.Rule = from timeSpan in TimeGrammar.TimeSpan
                              from suffix in Choice(Token(TimeLexer.FromNowTime), Token(TimeLexer.AgoTime))
                              let adjusted = suffix.Kind.Name == "past" ? timeSpan.Negate() : timeSpan
                              select new Time(adjusted);

            Time.Rule = Choice(TimePrefix, TimeSuffix);
        }
    }
}