using System;
using Code.Pizza.Core.Builders;
using Code.Pizza.Core.Domain;
using Code.Pizza.Core.Services.Interfaces;
using NUnit.Framework;
using Rhino.Mocks;

namespace Code.Pizza.Core.Tests
{
    [TestFixture]
    public class UserBuilderTests
    {
        [Test]
        public void Create_when_SetEmail_has_not_been_called_throws_InvalidOperationException()
        {
            // arrange
            ICryptoService cryptoService = MockRepository.GenerateStub<ICryptoService>();

            UserBuilder builder = new UserBuilder(cryptoService)
                .SetName("Name")
                .SetSurname("Surname")
                .SetPassword("password");

            // assert
            Assert.Throws<InvalidOperationException>(() => builder.Create());
        }

        [Test]
        public void Create_when_SetName_has_not_been_called_throws_InvalidOperationException()
        {
            // arrange
            ICryptoService cryptoService = MockRepository.GenerateStub<ICryptoService>();

            UserBuilder builder = new UserBuilder(cryptoService)
                .SetEmail("Email")
                .SetSurname("Surname")
                .SetPassword("password");

            // assert
            Assert.Throws<InvalidOperationException>(() => builder.Create());
        }

        [Test]
        public void Create_when_SetSurname_has_not_been_called_throws_InvalidOperationException()
        {
            // arrange
            ICryptoService cryptoService = MockRepository.GenerateStub<ICryptoService>();

            UserBuilder builder = new UserBuilder(cryptoService)
                .SetEmail("Email")
                .SetName("Name")
                .SetPassword("password");

            // assert
            Assert.Throws<InvalidOperationException>(() => builder.Create());
        }

        [Test]
        public void Create_when_SetPassword_has_not_been_called_throws_InvalidOperationException()
        {
            // arrange
            ICryptoService cryptoService = MockRepository.GenerateStub<ICryptoService>();

            UserBuilder builder = new UserBuilder(cryptoService)
                .SetEmail("Email")
                .SetName("Name")
                .SetSurname("Surname");

            // assert
            Assert.Throws<InvalidOperationException>(() => builder.Create());
        }

        [Test]
        public void Create_given_required_fluent_parameters_returns_new_User()
        {
            // arrange
            ICryptoService cryptoService = MockRepository.GenerateStub<ICryptoService>();
            cryptoService.Expect(x => x.GenerateSalt("password")).Return("salt");
            cryptoService.Expect(x => x.ComputeHash("salt", "password")).Return("hash");

            // act
            User user = new UserBuilder(cryptoService)
                .SetEmail("Email")
                .SetName("Name")
                .SetSurname("Surname")
                .SetPassword("password")
                .Create();

            // assert
            Assert.That(user, Is.Not.Null);
            Assert.That(user.Email == "Email");
            Assert.That(user.Name == "Name");
            Assert.That(user.Surname == "Surname");
            Assert.That(user.PasswordSalt == "salt");
            Assert.That(user.PasswordHash == "hash");

            cryptoService.VerifyAllExpectations();
        }
    }
}
