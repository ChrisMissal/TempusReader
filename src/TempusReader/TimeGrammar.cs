using System;
using System.Collections.Generic;
using System.Globalization;
using Parsley;

namespace TempusReader
{
    internal class TimeGrammar : Grammar
    {
        private static readonly GrammarRule<double> Amount = new GrammarRule<double>();
        private static readonly GrammarRule<object> Days = new GrammarRule<object>();
        private static readonly GrammarRule<object> Hours = new GrammarRule<object>();
        private static readonly GrammarRule<object> Minutes = new GrammarRule<object>();
        private static readonly GrammarRule<object> Seconds = new GrammarRule<object>();
        private static readonly GrammarRule<object> Milliseconds = new GrammarRule<object>(); 
        private static readonly GrammarRule<object> TimeFrame = new GrammarRule<object>();
        private static readonly GrammarRule<KeyValuePair<string, double>> Pair = new GrammarRule<KeyValuePair<string, double>>();
        public static readonly GrammarRule<TimeSpan> TimeSpan = new GrammarRule<TimeSpan>();

        static TimeGrammar()
        {
            Amount.Rule = from number in Token(TimeLexer.Number)
                          select Double.Parse(number.Literal, NumberStyles.Any);

            Days.Rule = from token in Token(TimeLexer.Days)
                        select token;

            Hours.Rule = from token in Token(TimeLexer.Hours)
                         select token;

            Minutes.Rule = from token in Token(TimeLexer.Minutes)
                           select token;

            Seconds.Rule = from token in Token(TimeLexer.Seconds)
                           select token;

            Milliseconds.Rule = from token in Token(TimeLexer.Milliseconds)
                                select token;

            TimeFrame.Rule = Choice(Days, Hours, Minutes, Seconds, Milliseconds);

            Pair.Rule =
                from amount in Amount
                from timeframe in TimeFrame
                let kind = ((Token)timeframe).Kind
                select new KeyValuePair<string, double>(kind.Name, amount);

            TimeSpan.Rule =
                from pair in Pair
                select Time.FromDatePart(pair.Key, pair.Value);
        }
    }
}