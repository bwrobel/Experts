using System;
using System.Security.Cryptography;
using System.Text;

namespace Experts.Core.Utils
{
    public class CryptoHelper
    {
        public static string CreateHash(string value, string salt)
        {
            var saltedPassword = value + salt;
            var hash = new SHA1CryptoServiceProvider().ComputeHash(new UTF8Encoding().GetBytes(saltedPassword));
            return Convert.ToBase64String(hash);
        }

        public static string CreateSalt()
        {
            var rng = new RNGCryptoServiceProvider();
            var byteArr = new byte[32];
            rng.GetBytes(byteArr);

            return Convert.ToBase64String(byteArr);
        }
    }
}
