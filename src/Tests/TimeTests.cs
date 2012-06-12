using System;
using System.Collections;
using NUnit.Framework;
using Shouldly;
using TempusReader;

namespace Tests
{
    [TestFixture]
    public class TimeTests
    {
        [TestCaseSource("TestData")]
        public Time Time_from_string(string value)
        {
            return new Time(value);
        }

        public IEnumerable TestData
        {
            get
            {
                yield return new TestCaseData("1 minute").Returns(1.Minutes());
                yield return new TestCaseData("1 minutes").Returns(1.Minutes());
                yield return new TestCaseData("0 minutes").Returns(0.Minutes());
                yield return new TestCaseData("90 minutes").Returns(90.Minutes());
                yield return new TestCaseData("15 min").Returns(15.Minutes());
                yield return new TestCaseData("45 mins").Returns(45.Minutes());

                yield return new TestCaseData("8 seconds").Returns(8.Seconds());
                yield return new TestCaseData("1 second").Returns(1.Seconds());
                yield return new TestCaseData("123 second").Returns(123.Seconds());
                yield return new TestCaseData("4 s").Returns(4.Seconds());
                yield return new TestCaseData("82 sec").Returns(82.Seconds());

                yield return new TestCaseData("1 day").Returns(1.Days());
                yield return new TestCaseData("2 days").Returns(2.Days());
                yield return new TestCaseData("3 d").Returns(3.Days());

                yield return new TestCaseData("12 hours").Returns(12.Hours());
                yield return new TestCaseData("3 hour").Returns(3.Hours());
                yield return new TestCaseData("9 hr").Returns(9.Hours());

                yield return new TestCaseData("21 milliseconds").Returns(21.Milliseconds());
                yield return new TestCaseData("500 ms").Returns(500.Milliseconds());
            }
        }

        [Test]
        public void Time_can_be_cast_to_TimeSpan()
        {
            var time = new Time("18 minutes");
            TimeSpan timeSpan = time;

            timeSpan.Minutes.ShouldBe(18);
        }
    }
}