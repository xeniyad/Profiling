using System;

namespace PasswordGeneration
{
    class Program
    {
        static void Main(string[] args)
        {
            var passwordGenerator = new PasswordGenerator();
            var salt = new byte[] { 10, 15, 23, 76, 23, 56, 98, 76, 87, 90, 45, 56, 87, 02, 99, 56,54, 76 };
            var password = passwordGenerator.GeneratePasswordHashUsingSalt("Qe2!w3Epsf_4r@", salt);
            Console.WriteLine(password);
            var password2 = passwordGenerator.GeneratePasswordHashUsingSalt("Qe2!w3Epffddsf_4r@", salt);
            Console.WriteLine(password2);
        }
    }
}
