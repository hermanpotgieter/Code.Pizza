using System;
using System.Collections.Generic;
using Code.Pizza.Common.EqualityComparers;
using NUnit.Framework;

namespace Code.Pizza.Common.Tests.EqualityComparers
{
    [TestFixture]
    public class LambdaComparerTests
    {
        [Test]
        public void Constructor_given_null_func_throws_ArgumentNullException()
        {
            // assert
            Assert.Throws<ArgumentNullException>(() => new LambdaComparer<object>(null));
        }

        [Test]
        public void Equals_given_different_references_returns_False()
        {
            // arrange
            object object1 = new object();
            object object2 = new object();

            IEqualityComparer<object> comparer = new LambdaComparer<object>((x, y) => x.Equals(y));

            // act
            bool equals = comparer.Equals(object1, object2);

            // assert
            Assert.IsFalse(equals);
        }

        [Test]
        public void Equals_given_same_reference_returns_True()
        {
            // arrange
            object object1 = new object();
            object object2 = object1;

            IEqualityComparer<object> comparer = new LambdaComparer<object>((x, y) => x.Equals(y));

            // act
            bool equals = comparer.Equals(object1, object2);

            // assert
            Assert.IsTrue(equals);
        }

        [Test]
        public void Equals_given_false_predicate_returns_False()
        {
            // arrange
            DateTime dateTime1 = DateTime.MinValue;
            DateTime dateTime2 = DateTime.MaxValue;

            IEqualityComparer<DateTime> comparer = new LambdaComparer<DateTime>((x, y) => x.Equals(y));

            // act
            bool equals = comparer.Equals(dateTime1, dateTime2);

            // assert
            Assert.IsFalse(equals);
        }

        [Test]
        public void Equals_given_true_predicate_returns_True()
        {
            // arrange
            DateTime dateTime1 = DateTime.Today;
            DateTime dateTime2 = DateTime.Today;

            IEqualityComparer<DateTime> comparer = new LambdaComparer<DateTime>((x, y) => x.Equals(y));

            // act
            bool equals = comparer.Equals(dateTime1, dateTime2);

            // assert
            Assert.IsTrue(equals);
        }
    }
}
