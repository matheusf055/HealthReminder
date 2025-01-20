using System.Threading.Tasks;
using HealthReminder.AppService.Users.DTOs;
using HealthReminder.Domain.Users;

namespace HealthReminder.AppService.Interfaces
{
    public interface IAuthAppService
    {
        Task<User> LoginAsync(LoginUserDto loginUserDto);
        Task RegisterAsync(RegisterUserDto registerUserDto);
    }
}