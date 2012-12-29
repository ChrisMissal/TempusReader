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
        [TestCaseSource("EnglishWordsValueTestData")]
        [TestCaseSource("MixedCaseValueTestData")]
        [TestCaseSource("MultipleAndFractionalValueTestData")]
        [TestCaseSource("FractionalValueTestData")]
        [TestCaseSource("MultiValueTestData")]
        [TestCaseSource("SingleValueTestData")]
        public Time Time_from_string(string value)
        {
            return new Time(value);
        }

        public static IEnumerable EnglishWordsValueTestData
        {
            get
            {
                yield return new TestCaseData("one hour")
                    .Returns(new Time(New(hours: 1)));
                yield return new TestCaseData("two minutes")
                    .Returns(new Time(New(minutes: 2)));
                yield return new TestCaseData("three seconds")
                    .Returns(new Time(New(seconds: 3)));
                yield return new TestCaseData("four ms")
                    .Returns(new Time(New(milliseconds: 4)));
                yield return new TestCaseData("five hrs")
                    .Returns(new Time(New(hours: 5)));
                yield return new TestCaseData("six mins")
                    .Returns(new Time(New(minutes: 6)));
                yield return new TestCaseData("seven sec")
                    .Returns(new Time(New(seconds: 7)));
                yield return new TestCaseData("eight milliseconds")
                    .Returns(new Time(New(milliseconds: 8)));
                yield return new TestCaseData("nine hours")
                    .Returns(new Time(New(hours: 9)));
                yield return new TestCaseData("ten minutes")
                    .Returns(new Time(New(minutes: 10)));
            }
        }

        public static IEnumerable MixedCaseValueTestData
        {
            get
            {
                yield return new TestCaseData("13 Hours and 14 MINs")
                    .Returns(new Time(New(hours: 13, minutes: 14)));
                yield return new TestCaseData("45 SECONDS and 50 miLLiseconds")
                    .Returns(new Time(New(seconds: 45, milliseconds: 50)));
            }
        }
        public static IEnumerable MultipleAndFractionalValueTestData
        {
            get
            {
                yield return new TestCaseData("3:45 hours and 2.5 mins")
                    .Returns(new Time(New(hours: 3, minutes: 47, seconds: 30)));
                yield return new TestCaseData("2.25 days, 4 hours, 90 mins")
                    .Returns(new Time(New(days: 2, hours: 11, minutes: 30)));
            }
        }

        public static IEnumerable FractionalValueTestData
        {
            get
            {
                yield return new TestCaseData("4:15 hrs")
                    .Returns(new Time(New(hours: 4, minutes: 15)));
                yield return new TestCaseData("2.18 seconds")
                    .Returns(new Time(New(seconds: 2, milliseconds: 180)));
                yield return new TestCaseData("6.5 minutes")
                    .Returns(new Time(New(minutes: 6, seconds: 30)));
            }
        }

        public static IEnumerable MultiValueTestData
        {
            get
            {
                yield return new TestCaseData("2 days, 7 hours, 12 mins and 52 seconds")
                    .Returns(new Time(New(days: 2, hours: 07, minutes: 12, seconds: 52)));

                yield return new TestCaseData("4 days and 21 minutes")
                    .Returns(new Time(New(days: 4, minutes: 21)));
            }
        }

        public static IEnumerable SingleValueTestData
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

        public static TimeSpan New(int days = 0, int hours = 0, int minutes = 0, int seconds = 0, int milliseconds = 0)
        {
            // because these should be optional anyway -_-
            return new TimeSpan(days, hours, minutes, seconds, milliseconds);
        }
    }
}