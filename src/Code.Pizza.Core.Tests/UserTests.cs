using System;
using Code.Pizza.Core.Domain;
using Code.Pizza.Core.Services.Interfaces;
using NUnit.Framework;
using Rhino.Mocks;

namespace Code.Pizza.Core.Tests
{
    [TestFixture]
    public class UserTests
    {
        [Test]
        public void Login_given_null_CryptoService_throws_ArgumentNullException()
        {
            // arrange
            User user = new User();

            // assert
            Assert.Throws<ArgumentNullException>(() => user.Login(null, "password"));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Login_given_null_or_whitespace_password_throws_ArgumentException(string password)
        {
            // arrange
            ICryptoService cryptoService = MockRepository.GenerateStub<ICryptoService>();
            User user = new User();

            // assert
            Assert.Throws<ArgumentException>(() => user.Login(cryptoService, password));
        }

        [Test]
        public void Login_when_successful_returns_true_and_sets_LastActive()
        {
            // arrange
            ICryptoService cryptoService = MockRepository.GenerateStub<ICryptoService>();
            cryptoService.Expect(x => x.VerifyPassword("salt", "hash", "password")).Return(true);

            User user = new User
                        {
                            Email = "Email",
                            PasswordSalt = "salt",
                            PasswordHash = "hash",
                        };

            // act
            bool loggedIn = user.Login(cryptoService, "password");

            // assert
            Assert.IsTrue(loggedIn);

            cryptoService.VerifyAllExpectations();
        }

        [Test]
        public void Login_when_unsuccessful_returns_false_and_sets_LastActive()
        {
            // arrange
            ICryptoService cryptoService = MockRepository.GenerateStub<ICryptoService>();
            cryptoService.Expect(x => x.VerifyPassword("salt", "hash", "password")).Return(false);

            User user = new User
                        {
                            Email = "Email",
                            PasswordSalt = "salt",
                            PasswordHash = "hash",
                        };

            // act
            bool loggedIn = user.Login(cryptoService, "password");

            // assert
            Assert.IsFalse(loggedIn);

            cryptoService.VerifyAllExpectations();
        }
    }
}
