using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using Parsley;

namespace TempusReader
{
    internal class TempusGrammar : Grammar
    {
        private static readonly TextToDigits TextToDigits = new TextToDigits();

        private static readonly Regex TimePartRegex = new Regex(@"(?<whole>\d+)\:(?<fraction>\d+)", RegexOptions.Singleline | RegexOptions.CultureInvariant | RegexOptions.Compiled);
        private static readonly GrammarRule<double> Amount = new GrammarRule<double>();
        private static readonly GrammarRule<Token> TimeFrame = new GrammarRule<Token>();
        private static readonly GrammarRule<IEnumerable<Token>> Break = new GrammarRule<IEnumerable<Token>>();
        private static readonly GrammarRule<KeyValuePair<string, double>> Pair = new GrammarRule<KeyValuePair<string, double>>();
        public static readonly GrammarRule<TimeSpan> TimeSpan = new GrammarRule<TimeSpan>();

        private static readonly GrammarRule<Time> TimePrefix = new GrammarRule<Time>();
        private static readonly GrammarRule<Time> TimeSuffix = new GrammarRule<Time>();
        public static readonly GrammarRule<Time> Yesterday = new GrammarRule<Time>();
        public static readonly GrammarRule<Time> Tomorrow = new GrammarRule<Time>();
        public static readonly GrammarRule<Time> Time = new GrammarRule<Time>();

        static TempusGrammar()
        {
            TimePrefix.Rule = from prefix in Choice(Token(TimeLexer.InTime))
                              from timeSpan in TimeSpan
                              select new Time(timeSpan);

            TimeSuffix.Rule = from timeSpan in TimeSpan
                              from suffix in Choice(Token(TimeLexer.FromNowTime), Token(TimeLexer.AgoTime))
                              let adjusted = suffix.Kind.Name == "past" ? timeSpan.Negate() : timeSpan
                              select new Time(adjusted);

            Yesterday.Rule = from _ in Token(TimeLexer.Yesterday) 
                             select new Time(System.TimeSpan.FromDays(-1));

            Tomorrow.Rule = from _ in Token(TimeLexer.Tomorrow)
                            select new Time(System.TimeSpan.FromDays(1));

            Time.Rule = Choice(TimePrefix, TimeSuffix, Yesterday, Tomorrow);

            Break.Rule = ZeroOrMore(Token(TimeLexer.Separator), Token(TimeLexer.Whitespace));

            Amount.Rule = from number in Choice(Token(TimeLexer.NumberWord), Token(TimeLexer.Number))
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
                let parts = TempusReader.Time.FromDateParts(pairs)
                select parts;
        }

        private static double ParseAmount(Token number)
        {
            var textDouble = TextToDigits.GetDefaultValue(number.Literal);
            if (textDouble > 0d)
                return textDouble;

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