using System;
using Code.Pizza.Common.Extensions;
using NUnit.Framework;

namespace Code.Pizza.Common.Tests.Extensions
{
    [TestFixture]
    public class EnumExtensionTests
    {
        [Test]
        public void Description_given_emum_without_DescriptionAttribute_returns_enum_ToString()
        {
            // arrange
            const string expected = "TestPassed";
            Enum testEnum = TestOutcome.TestPassed;

            // act
            string actual = testEnum.Description();

            // assert
            Assert.That(actual.Equals(expected, StringComparison.InvariantCulture));
        }

        [Test]
        public void Description_given_emum_with_DescriptionAttribute_returns_Description()
        {
            // arrange
            const string expected = "Test Passed";
            Enum testEnum = AttributedTestOutcome.TestPassed;

            // act
            string actual = testEnum.Description();

            // assert
            Assert.That(actual.Equals(expected, StringComparison.InvariantCulture));
        }
    }

    internal enum TestOutcome
    {
        TestPassed,
        TestFailed
    }

    internal enum AttributedTestOutcome
    {
        [System.ComponentModel.Description("Test Passed")]
        TestPassed,

        [System.ComponentModel.Description("Test Failed")]
        TestFailed
    }
}
