using System.Threading.Tasks;

namespace HealthReminder.Domain.Users.Repositories
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserDetailsAsync(Guid id);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(Guid id);
    }
}