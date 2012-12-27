using System;
using System.Collections;
using NUnit.Framework;
using TempusReader;

namespace Tests
{
    [TestFixture]
    public class DateTests
    {
        public static readonly DateTime BaseDate = new DateTime(1982, 10, 21, 23, 40, 0);

        [TestCaseSource("RelativeTimeTestData")]
        public Date Date_from_string(string input)
        {
            return new Date(BaseDate, input);
        }

        public static IEnumerable RelativeTimeTestData
        {
            get
            {
                yield return new TestCaseData("in 10 minutes")
                    .Returns(new Date(BaseDate.AddMinutes(10)));
                yield return new TestCaseData("4 hrs from now")
                    .Returns(new Date(BaseDate.AddHours(4)));
                yield return new TestCaseData("45 seconds ago")
                    .Returns(new Date(BaseDate.AddSeconds(-45)));
                yield return new TestCaseData("yesterday")
                    .Returns(new Date(BaseDate.AddDays(-1)));
                yield return new TestCaseData("tomorrow")
                    .Returns(new Date(BaseDate.AddDays(1)));
            }
        }
    }
}