using HealthReminder.Domain.Common.Auditable;
using HealthReminder.Domain.Common.Entities;
using System.Security.Cryptography;

namespace HealthReminder.Domain.Users
{
    public class User : EntityBase
    {
        public User() { }

        public User(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Salt = GenerateSalt();
            Password = HashPasswordWithSalt(password, Salt);
            CreateDate = DateTime.UtcNow;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; private set; }
        public DateTime CreateDate { get; set; }

        public bool VerifyPassword(string providedPassword)
        {
            var hashedProvidedPassword = HashPasswordWithSalt(providedPassword, Salt);
            return Password == hashedProvidedPassword;
        }

        private static string GenerateSalt()
        {
            var saltBytes = new byte[16];
            RandomNumberGenerator.Fill(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }

        private static string HashPasswordWithSalt(string password, string salt, int iterations = 10000)
        {
            var saltedPassword = password + salt;
            var hash = Common.Security.HashAlgorithm.SHA256(saltedPassword);
            for (int i = 0; i < iterations; i++)
            {
                hash = Common.Security.HashAlgorithm.SHA256(hash);
            }
            return hash;
        }
    }
}
