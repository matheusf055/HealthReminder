using System.Threading.Tasks;
using HealthReminder.AppService.Auth.DTOs;
using HealthReminder.Domain.Users;

namespace HealthReminder.AppService.Interfaces.Auth
{
    public interface IAuthAppService
    {
        Task<Domain.Users.User> LoginAsync(LoginUserDto loginUserDto);
        Task RegisterAsync(RegisterUserDto registerUserDto);
    }
}