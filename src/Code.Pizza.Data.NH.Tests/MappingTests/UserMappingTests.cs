using Code.Pizza.Core.Domain;
using NHibernate;
using NUnit.Framework;

namespace Code.Pizza.Data.NH.Tests.MappingTests
{
    [TestFixture]
    public class UserMappingTests : TestBase
    {
        [Test]
        public void UserMappingTest()
        {
            // arrange
            User expected = new User
                            {
                                PasswordSalt = "PasswordSalt",
                                PasswordHash = "PasswordHash",
                                Email = "Email",
                            };

            // act
            using(ISession session = this.sessionFactory.OpenSession())
            {
                using(ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(expected);
                    transaction.Commit();
                }
            }

            User actual;

            using(ISession session = this.sessionFactory.OpenSession())
            {
                using(session.BeginTransaction())
                {
                    actual = session.Get<User>(expected.ID);
                }
            }

            // assert
            Assert.That(actual != expected);
            Assert.That(actual.ID == expected.ID);
            Assert.That(actual.PasswordSalt == expected.PasswordSalt);
            Assert.That(actual.PasswordHash == expected.PasswordHash);
        }
    }
}
