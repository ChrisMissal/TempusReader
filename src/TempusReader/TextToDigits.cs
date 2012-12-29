using System;
using System.Collections.Generic;
using System.Linq;

namespace TempusReader
{
    internal class TextToDigits
    {
        private static readonly string[] textValues = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten" };

        public double GetDefaultValue(string text)
        {
            return Array.FindIndex(textValues, x => x.Equals(text, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}