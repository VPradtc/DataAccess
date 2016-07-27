using System;
using System.Security.Cryptography;
using System.Text;

namespace DataAccess.IdentityTokenCore.Authentication.Helpers
{
    public class SecurityHelper
    {
        public static string GetHash(string input)
        {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();

            var byteValue = Encoding.UTF8.GetBytes(input);

            var byteHash = hashAlgorithm.ComputeHash(byteValue);

            return Convert.ToBase64String(byteHash);
        }

        public static string GetRefreshTokenId()
        {
            return Guid.NewGuid().ToString("n");
        }
    }
}
