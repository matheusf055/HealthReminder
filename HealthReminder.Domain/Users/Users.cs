using HealthReminder.Domain.Common.Entities;
using System.Security.Cryptography;

namespace HealthReminder.Domain.Users
{
    public class Users : EntityBase
    {
        public Users() { }

        public Users(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
            CreateDate = DateTime.UtcNow;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
