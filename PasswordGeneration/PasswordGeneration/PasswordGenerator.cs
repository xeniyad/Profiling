using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace PasswordGeneration
{
    public class PasswordGenerator
    {
        const int iterate = 10000;

        public string GeneratePasswordHashUsingSalt(string passwordText, byte[] salt)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(passwordText, salt, iterate);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Buffer.BlockCopy(salt, 0, hashBytes, 0, 16);
            Buffer.BlockCopy(hash, 0, hashBytes, 16, 20);

            var passwordHash = Convert.ToBase64String(hashBytes);



            return passwordHash;

        }
    }
}
