using System.Security.Cryptography;
using System.Text;

namespace HealthReminder.Domain.Common.Security
{
    public class HashAlgorithm
    {
        public static string SHA256(string input)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var inputBytes = Encoding.UTF8.GetBytes(input);
                var hashBytes = sha256.ComputeHash(inputBytes);

                var sb = new StringBuilder();
                foreach (byte b in hashBytes)
                    sb.Append(b.ToString("x2"));

                return sb.ToString();
            }
        }

        public static string GenerateSalt()
        {
            var saltBytes = new byte[16];
            RandomNumberGenerator.Fill(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }

        public static string HashPasswordWithSalt(string password, string salt, int iterations = 10000)
        {
            var saltedPassword = password + salt;
            var hash = SHA256(saltedPassword);
            for (int i = 0; i < iterations; i++)
            {
                hash = SHA256(hash);
            }
            return hash;
        }
    }
}
