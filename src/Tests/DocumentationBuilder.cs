using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Shouldly;

namespace Tests
{
    [TestFixture]
    public class DocumentationBuilder
    {
        [TestCase("TimeTests")]
        [TestCase("DateTests")]
        [Explicit("Only used to run to output some text to be used in documentation.")]
        public void Generate_examples_from_tests(string testfixture)
        {
            var startLine = string.Format("<!--- {0} start -->", testfixture);
            var endLine = string.Format("<!--- {0} end -->", testfixture);
            var fileName = "../../../../README.md";

            File.Exists(fileName).ShouldBe(true);

            var lines = File.ReadAllLines(fileName);

            try
            {
                var top = lines.TakeWhile(l => l != startLine).ToArray();
                var bottomAndEndLine = lines.SkipWhile(l => l != endLine).ToArray();

                var documentLines = GenerateDocumentationLinesFromTestData(testfixture);

                var updated = top.Concat(new[] { startLine })
                    .Concat(documentLines)
                    .Concat(bottomAndEndLine).ToArray();

                top.ShouldNotBeEmpty();
                bottomAndEndLine.ShouldNotBeEmpty();
                updated.ShouldNotBeEmpty();

                File.WriteAllLines(fileName, updated);
            }
            catch (Exception ex)
            {
                File.WriteAllLines(fileName, lines);
                Assert.Fail(ex.Message);
            }
        }

        private IEnumerable<string> GenerateDocumentationLinesFromTestData(string testfixture)
        {
            if (testfixture == "TimeTests")
                return TimeTestDocumentation();

            return DateTestDocumentation();
        }

        private IEnumerable<string> DateTestDocumentation()
        {
            Func<TestCaseData, string> format = td => string.Format(@"    new Date(BaseDate, ""{0}"") // {2}{1}", td.Arguments[0], Environment.NewLine, td.Result);

            yield return "### Relative Date Values";
            yield return string.Format("    var BaseDate = new DateTime({0});{1}", DateTests.BaseDate.ToString("yyyy, M, d, H, m, s") , Environment.NewLine);
            foreach (var testData in DateTests.RelativeTimeTestData.Cast<TestCaseData>())
                yield return format(testData);
        }

        private IEnumerable<string> TimeTestDocumentation()
        {
            Func<TestCaseData, string> format = td => string.Format(@"    new Time(""{0}"") // {2}{1}", td.Arguments[0], Environment.NewLine, td.Result);

            yield return "### Single Values";
            foreach (var testData in TimeTests.SingleValueTestData.Cast<TestCaseData>())
                yield return format(testData);

            yield return "### Multiple Values";
            foreach (var testData in TimeTests.MultiValueTestData.Cast<TestCaseData>())
                yield return format(testData);

            yield return "### Fractional Values";
            foreach (var testData in TimeTests.FractionalValueTestData.Cast<TestCaseData>())
                yield return format(testData);

            yield return "### Multiple and Fractional Values";
            foreach (var testData in TimeTests.MultipleAndFractionalValueTestData.Cast<TestCaseData>())
                yield return format(testData);

            yield return "### Mixed Case (upper/lower) Values";
            foreach (var testData in TimeTests.MixedCaseValueTestData.Cast<TestCaseData>())
                yield return format(testData);

            yield return "### English Words to Values";
            foreach (var testData in TimeTests.EnglishWordsValueTestData.Cast<TestCaseData>())
                yield return format(testData);
        }

    }
}