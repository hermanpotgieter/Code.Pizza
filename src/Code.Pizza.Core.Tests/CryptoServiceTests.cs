using System;
using Code.Pizza.Core.Services.Impl;
using NUnit.Framework;

namespace Code.Pizza.Core.Tests
{
    [TestFixture]
    public class CryptoServiceTests
    {
        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void GenerateSalt_given_null_or_whitespace_password_throws_ArgumentException(string password)
        {
            // arrange
            CryptoService cryptoService = new CryptoService();

            // assert
            Assert.Throws<ArgumentException>(() => cryptoService.GenerateSalt(password));
        }

        [Test]
        public void GenerateSalt_given_password_returns_salt()
        {
            // arrange
            CryptoService cryptoService = new CryptoService();

            // act
            string salt = cryptoService.GenerateSalt("password");

            // assert
            Assert.That(salt, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void GenerateSalt_called_repeatedly_with_same_password_returns_different_salts()
        {
            // arrange
            CryptoService cryptoService = new CryptoService();

            // act
            string salt1 = cryptoService.GenerateSalt("password");
            string salt2 = cryptoService.GenerateSalt("password");

            // assert
            Assert.That(salt1, Is.Not.Null.Or.Empty);
            Assert.That(salt2, Is.Not.Null.Or.Empty);
            Assert.That(salt1 != salt2);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void ComputeHash_given_null_or_whitespace_salt_throws_ArgumentException(string salt)
        {
            // arrange
            CryptoService cryptoService = new CryptoService();

            // assert
            Assert.Throws<ArgumentException>(() => cryptoService.ComputeHash(salt, "password"));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void ComputeHash_given_null_or_whitespace_password_throws_ArgumentException(string password)
        {
            // arrange
            CryptoService cryptoService = new CryptoService();

            // assert
            Assert.Throws<ArgumentException>(() => cryptoService.ComputeHash("salt", password));
        }

        [Test]
        public void ComputeHash_given_salt_and_password_returns_hash()
        {
            // arrange
            CryptoService cryptoService = new CryptoService();

            // act
            string hash = cryptoService.ComputeHash("R9WRwHWu+SXABMLfG+2i3rAb6BmsbDf0rFnm0XkSyzk=", "password");

            // assert
            Assert.That(hash, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void ComputeHash_given_same_salt_with_different_passwords_returns_different_hashes()
        {
            // arrange
            CryptoService cryptoService = new CryptoService();

            // act
            string hash1 = cryptoService.ComputeHash("R9WRwHWu+SXABMLfG+2i3rAb6BmsbDf0rFnm0XkSyzk=", "password1");
            string hash2 = cryptoService.ComputeHash("R9WRwHWu+SXABMLfG+2i3rAb6BmsbDf0rFnm0XkSyzk=", "password2");

            // assert
            Assert.That(hash1, Is.Not.Null.Or.Empty);
            Assert.That(hash2, Is.Not.Null.Or.Empty);
            Assert.That(hash1 != hash2);
        }

        [Test]
        public void ComputeHash_given_different_salts_with_same_password_returns_different_hashes()
        {
            // arrange
            CryptoService cryptoService = new CryptoService();

            // act
            string hash1 = cryptoService.ComputeHash("R9WRwHWu+SXABMLfG+2i3rAb6BmsbDf0rFnm0XkSyzk=", "password");
            string hash2 = cryptoService.ComputeHash("lqeCOZp4gkbl5AjS80FNbYjr+HU9GGS2wtKo9HRQm+I=", "password");

            // assert
            Assert.That(hash1, Is.Not.Null.Or.Empty);
            Assert.That(hash2, Is.Not.Null.Or.Empty);
            Assert.That(hash1 != hash2);
        }

        [Test]
        public void ComputeHash_called_repeatedly_with_same_salt_and_password_returns_same_hash()
        {
            // arrange
            CryptoService cryptoService = new CryptoService();

            // act
            string hash1 = cryptoService.ComputeHash("R9WRwHWu+SXABMLfG+2i3rAb6BmsbDf0rFnm0XkSyzk=", "password");
            string hash2 = cryptoService.ComputeHash("R9WRwHWu+SXABMLfG+2i3rAb6BmsbDf0rFnm0XkSyzk=", "password");

            // assert
            Assert.That(hash1, Is.Not.Null.Or.Empty);
            Assert.That(hash2, Is.Not.Null.Or.Empty);
            Assert.That(hash1 == hash2);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void VerifyPassword_given_null_or_whitespace_salt_throws_ArgumentException(string salt)
        {
            // arrange
            CryptoService cryptoService = new CryptoService();

            // assert
            Assert.Throws<ArgumentException>(() => cryptoService.VerifyPassword(salt, "hash", "password"));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void VerifyPassword_given_null_or_whitespace_hash_throws_ArgumentException(string hash)
        {
            // arrange
            CryptoService cryptoService = new CryptoService();

            // assert
            Assert.Throws<ArgumentException>(() => cryptoService.VerifyPassword("salt", hash, "password"));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void VerifyPassword_given_null_or_whitespace_password_throws_ArgumentException(string password)
        {
            // arrange
            CryptoService cryptoService = new CryptoService();

            // assert
            Assert.Throws<ArgumentException>(() => cryptoService.VerifyPassword("salt", "hash", password));
        }

        [Test]
        public void VerifyPassword_given_incorrect_password_returns_false()
        {
            // arrange
            CryptoService cryptoService = new CryptoService();

            // act
            bool verifyPassword = cryptoService.VerifyPassword("R9WRwHWu+SXABMLfG+2i3rAb6BmsbDf0rFnm0XkSyzk=",
                                                               "4art8TSw2hi4hgu2EN0DrSnvOX5/sjFs8IyJxm4j/VQ=",
                                                               "incorrect");

            // assert
            Assert.IsFalse(verifyPassword);
        }

        [Test]
        public void VerifyPassword_given_correct_password_returns_true()
        {
            // arrange
            CryptoService cryptoService = new CryptoService();

            // act
            bool verifyPassword = cryptoService.VerifyPassword("R9WRwHWu+SXABMLfG+2i3rAb6BmsbDf0rFnm0XkSyzk=",
                                                               "4art8TSw2hi4hgu2EN0DrSnvOX5/sjFs8IyJxm4j/VQ=",
                                                               "password");

            // assert
            Assert.IsTrue(verifyPassword);
        }

        [Test]
        [Ignore]
        public void Generator()
        {
            CryptoService cryptoService = new CryptoService();

            const string password = "123";

            string salt = cryptoService.GenerateSalt(password);
            string hash = cryptoService.ComputeHash(salt, password);

            Console.WriteLine("{0} {1}", salt, hash);
        }
    }
}
