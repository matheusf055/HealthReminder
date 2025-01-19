using HealthReminder.Domain.Common.Auditable;
using HealthReminder.Domain.Common.Entities;
using HealthReminder.Domain.Common.Security;

namespace HealthReminder.Domain.Users
{
    public class User : EntityBase, IUpdateAuditable
    {
        public User(string name, string email, string password, string confirmPassword)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name), "Digite seu nome");
            Email = email ?? throw new ArgumentNullException(nameof(email), "Digite seu email");

            if (password != confirmPassword)
            {
                throw new ArgumentException("As senhas não coincidem.");
            }

            Salt = HashAlgorithm.GenerateSalt();
            Password = HashAlgorithm.HashPasswordWithSalt(password, Salt);
            CreateDate = DateTime.UtcNow;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; private set; }

        #region auditable
        public DateTime CreateDate { get; set; }
        public Guid? UpdateUserId { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        #endregion

        public bool VerifyPassword(string providedPassword)
        {
            var hashedProvidedPassword = HashAlgorithm.HashPasswordWithSalt(providedPassword, Salt);
            return Password == hashedProvidedPassword;
        }
    }
}
