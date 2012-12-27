using System;
using System.Collections;
using NUnit.Framework;
using TempusReader;

namespace Tests
{
    [TestFixture]
    public class DateTests
    {
        private DateTime BaseDate { get; set; }

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            BaseDate = new DateTime(1982, 10, 21, 23, 40, 0);
        }

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
                    .Returns(new Date(new DateTime(1982, 10, 21, 23, 50, 0)));
                yield return new TestCaseData("4 hrs from now")
                    .Returns(new Date(new DateTime(1982, 10, 22, 3, 40, 0)));
                yield return new TestCaseData("45 seconds ago")
                    .Returns(new Date(new DateTime(1982, 10, 21, 23, 39, 15)));
                yield return new TestCaseData("yesterday")
                    .Returns(new Date(new DateTime(1982, 10, 20, 23, 40, 0)));
            }
        }
    }
}