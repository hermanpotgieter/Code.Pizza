using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Code.Pizza.Common.Utilities;
using Code.Pizza.Core.Services.Interfaces;

namespace Code.Pizza.Core.Services.Impl
{
    public class CryptoService : ICryptoService
    {
        private const int keyBytes = 32;
        private const int saltSize = 32;
        private const int iterations = 10000;

        public string GenerateSalt(string password)
        {
            Guard.AgainstNullOrWhiteSpaceString(() => password);

            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, saltSize, iterations);
            byte[] bytes = pbkdf2.GetBytes(keyBytes);

            string salt = Convert.ToBase64String(bytes);

            return salt;
        }

        public string ComputeHash(string salt, string password)
        {
            Guard.AgainstNullOrWhiteSpaceString(() => salt);
            Guard.AgainstNullOrWhiteSpaceString(() => password);

            byte[] saltBytes = Convert.FromBase64String(salt);

            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, iterations);
            byte[] bytes = pbkdf2.GetBytes(keyBytes);

            string hash = Convert.ToBase64String(bytes);

            return hash;
        }

        public bool VerifyPassword(string salt, string hash, string password)
        {
            Guard.AgainstNullOrWhiteSpaceString(() => salt);
            Guard.AgainstNullOrWhiteSpaceString(() => hash);
            Guard.AgainstNullOrWhiteSpaceString(() => password);

            byte[] saltBytes = Convert.FromBase64String(salt);
            byte[] hashBytes = Convert.FromBase64String(hash);

            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, iterations);
            byte[] passwordBytes = pbkdf2.GetBytes(keyBytes);

            bool verified = CompareBytes(hashBytes, passwordBytes);

            return verified;
        }

        private static bool CompareBytes(IList<byte> a, IList<byte> b)
        {
            uint diff = (uint)a.Count ^ (uint)b.Count;

            for(int i = 0; i < a.Count && i < b.Count; i++)
            {
                diff |= (uint)(a[i] ^ b[i]);
            }

            return diff == 0;
        }
    }
}
