using System;
using Code.Pizza.Common.Utilities;
using NUnit.Framework;

namespace Code.Pizza.Common.Tests.Utilities
{
    [TestFixture]
    public class GuardTests
    {
        [Test]
        public void Against_WithValidAssertion_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => Guard.Against<ArgumentNullException>(false, "Guard check with false assertion"));
        }

        [Test]
        public void Against_WithInvalidAssertion_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => Guard.Against<ArgumentNullException>(true, "Guard check with true assertion"));
        }

        [Test]
        public void Against_WithValidLambdaAssertion_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => Guard.Against<ArgumentNullException>(() => false, "Guard check with false Lambda"));
        }

        [Test]
        public void Against_WithInvalidLambdaAssertion_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => Guard.Against<ArgumentNullException>(() => true, "Guard check with true Lambda"));
        }

        [Test]
        public void AgainstNull_WithValidConstantExpression_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => Guard.AgainstNull<object>(() => false));
        }

        [Test]
        public void AgainstNull_WithValidMemberExpression_DoesNotThrow()
        {
            bool? testArgument = false;
            Assert.DoesNotThrow(() => Guard.AgainstNull(() => testArgument));
        }

        [Test]
        public void AgainstNull_WithValidLambdaExpression_DoesNotThrow()
        {
            NullReferenceException ex = new NullReferenceException();

            // valid cast, does not evaluate to null
            Assert.DoesNotThrow(() => Guard.AgainstNull(() => ex as Exception));
        }

        [Test]
        public void AgainstNull_WithInvalidConstantExpression_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => Guard.AgainstNull<object>(() => null));
        }

        [Test]
        public void AgainstNull_WithInvalidMemberExpression_ThrowsException()
        {
            bool? testArgument = null;
            Assert.Throws<ArgumentNullException>(() => Guard.AgainstNull(() => testArgument));
        }

        [Test]
        public void AgainstNull_WithInvalidLambdaExpression_ThrowsException()
        {
            Exception ex = new Exception();

            // will not cast, so evaluates to null
            Assert.Throws<ArgumentNullException>(() => Guard.AgainstNull(() => ex as NullReferenceException));
        }

        [Test]
        public void AgainstNullOrEmptyString_WithValidConstantExpression_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => Guard.AgainstNullOrEmptyString(() => "test"));
        }

        [Test]
        public void AgainstNullOrEmptyString_WithValidMemberExpression_DoesNotThrow()
        {
            string testArgument = bool.TrueString;
            Assert.DoesNotThrow(() => Guard.AgainstNullOrEmptyString(() => testArgument));
        }

        [Test]
        public void AgainstNullOrEmptyString_WithValidLambdaExpression_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => Guard.AgainstNullOrEmptyString(() => bool.TrueString));
        }

        [Test]
        public void AgainstNullOrEmptyString_WithTrueConstantExpression_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => Guard.AgainstNullOrEmptyString(() => null));
        }

        [Test]
        public void AgainstNullOrEmptyString_WithTrueMemberExpression_ThrowsException()
        {
            string testArgument = string.Empty;
            Assert.Throws<ArgumentException>(() => Guard.AgainstNullOrEmptyString(() => testArgument));
        }
    }
}