using System;
using Code.Pizza.Common.Utilities;
using NUnit.Framework;

namespace Code.Pizza.Common.Tests.Utilities
{
    [TestFixture]
    public class EnforceTests
    {
        private interface ITestable
        {
            void DoSomething();
        }

        private class BaseDummy : ITestable
        {
            public void DoSomething() {}
        }

        private class DerivedDummy : BaseDummy {}

        [Test]
        public void Implements_InstanceTypeDoesNotImplementInterface_ThrowsInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() => Enforce.Implements<ITestable>(new Exception(), "Enforce check with Implements"));
        }

        [Test]
        public void Implements_InstanceTypeImplementsInterface_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => Enforce.Implements<ITestable>(new BaseDummy(), "Enforce check with Implements"));
        }

        [Test]
        public void InheritsFrom_InstanceTypeIsNotDerived_ThrowsInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() => Enforce.InheritsFrom<BaseDummy>(new Exception(), "Enforce check with InheritsFrom"));
        }

        [Test]
        public void InheritsFrom_DerivedType_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => Enforce.InheritsFrom<BaseDummy>(new DerivedDummy(), "Enforce check with InheritsFrom"));
        }

        [Test]
        public void IsEqual_DifferentInstances_ThrowsException()
        {
            var thisInstance = new BaseDummy();
            var otherInstance = new BaseDummy();

            Assert.Throws<InvalidOperationException>(() => Enforce.IsEqual<InvalidOperationException>(thisInstance, otherInstance, "Enforce check with IsEqual"));
        }

        [Test]
        public void IsEqual_SameInstance_DoesNotThrow()
        {
            var thisInstance = new BaseDummy();
            var otherInstance = thisInstance;

            Assert.DoesNotThrow(() => Enforce.IsEqual<InvalidOperationException>(thisInstance, otherInstance, "Enforce check with IsEqual"));
        }

        [Test]
        public void TypeOf_InstanceTypeDoesNotMatch_ThrowsInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() => Enforce.TypeOf<InvalidOperationException>(new Exception(), "Enforce check with TypeOf"));
        }

        [Test]
        public void TypeOf_InstanceTypeDoesMatch_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => Enforce.TypeOf<InvalidOperationException>(new InvalidOperationException(), "Enforce check with TypeOf"));
        }
    }
}