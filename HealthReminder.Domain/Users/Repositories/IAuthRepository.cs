using System.Threading.Tasks;

namespace HealthReminder.Domain.Users.Repositories
{
    public interface IAuthRepository
    {
        Task AddUserAsync(User user);
        Task<User> GetUserByEmailAsync(string email);
    }
}