using System.Linq;
using Parsley;

namespace TempusReader
{
    internal class BaseLexer : Lexer
    {
        public static readonly TokenKind Separator = new Pattern("separator", @",|(and)", skippable: true);
        public static readonly TokenKind Whitespace = new Pattern("whitespace", @"\s+", skippable: true);
        public static readonly TokenKind Number = new Pattern("number", @"(?=0(?!\d)|[1-9])\d+((\.|\:)\d+)?");

        public BaseLexer(params TokenKind[] kinds) : base(new [] { Separator, Whitespace, Number}.Concat(kinds).ToArray())
        {
        }
    }
}