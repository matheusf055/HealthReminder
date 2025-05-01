using System.Threading.Tasks;

namespace HealthReminder.Domain.User.Repositories
{
    public interface IUserRepository
    {
        Task Create(Users user);
        Task<Users> GetById(Guid id);
        Task<Users> GetByEmail(string email);
        Task Delete(Guid id);
    }
}