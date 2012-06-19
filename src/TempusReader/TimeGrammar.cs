using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using Parsley;

namespace TempusReader
{
    internal class TimeGrammar : Grammar
    {
        private static readonly Regex TimePartRegex = new Regex(@"(?<whole>\d+)\:(?<fraction>\d+)", RegexOptions.Singleline | RegexOptions.CultureInvariant | RegexOptions.Compiled);
        private static readonly GrammarRule<double> Amount = new GrammarRule<double>();
        private static readonly GrammarRule<Token> TimeFrame = new GrammarRule<Token>();
        private static readonly GrammarRule<IEnumerable<Token>> Break = new GrammarRule<IEnumerable<Token>>();
        private static readonly GrammarRule<KeyValuePair<string, double>> Pair = new GrammarRule<KeyValuePair<string, double>>();
        public static readonly GrammarRule<TimeSpan> TimeSpan = new GrammarRule<TimeSpan>();

        static TimeGrammar()
        {
            Break.Rule = ZeroOrMore(Token(BaseLexer.Separator), Token(BaseLexer.Whitespace));

            Amount.Rule = from number in Token(BaseLexer.Number)
                          select ParseAmount(number);

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

        private static double ParseAmount(Token number)
        {
            var literal = number.Literal;
            var match = TimePartRegex.Match(literal);
            if (match.Success)
            {
                var whole = Int32.Parse(match.Groups["whole"].Value);
                var fraction = Int32.Parse(match.Groups["fraction"].Value);
                return whole + (fraction / 60d);
            }

            return Double.Parse(literal, NumberStyles.Any);
        }
    }
}