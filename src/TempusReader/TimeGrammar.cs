using System;
using System.Collections.Generic;
using System.Globalization;
using Parsley;

namespace TempusReader
{
    internal class TimeGrammar : Grammar
    {
        private static readonly GrammarRule<double> Amount = new GrammarRule<double>();
        private static readonly GrammarRule<Token> TimeFrame = new GrammarRule<Token>();
        private static readonly GrammarRule<IEnumerable<Token>> Break = new GrammarRule<IEnumerable<Token>>();
        private static readonly GrammarRule<KeyValuePair<string, double>> Pair = new GrammarRule<KeyValuePair<string, double>>();
        public static readonly GrammarRule<TimeSpan> TimeSpan = new GrammarRule<TimeSpan>();

        static TimeGrammar()
        {
            Break.Rule = ZeroOrMore(Token(TimeLexer.Separator), Token(TimeLexer.Whitespace));

            Amount.Rule = from number in Token(TimeLexer.Number)
                          select Double.Parse(number.Literal, NumberStyles.Any);

            TimeFrame.Rule = Choice(
                Token(TimeLexer.Days),
                Token(TimeLexer.Hours),
                Token(TimeLexer.Minutes),
                Token(TimeLexer.Seconds),
                Token(TimeLexer.Milliseconds));

            Pair.Rule =
                from amount in Amount
                from timeframe in TimeFrame
                select new KeyValuePair<string, double>(timeframe.Kind.Name, amount);

            TimeSpan.Rule =
                from pairs in ZeroOrMore(Pair, Break)
                let parts = Time.FromDateParts(pairs)
                select parts;
        }
    }
}