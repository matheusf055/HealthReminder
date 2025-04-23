using System.Threading.Tasks;

namespace HealthReminder.Domain.Users.Repositories
{
    public interface IUserRepository
    {
        Task AddUserAsync(Users user);
        Task<Users> GetUserByIdAsync(Guid id);
        Task<Users> GetUserByEmailAsync(string email);
        Task<Users> GetUserDetailsAsync(Guid id);
        Task UpdateUserAsync(Users user);
        Task DeleteUserAsync(Guid id);
    }
}