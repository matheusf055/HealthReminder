using System.Threading.Tasks;
using HealthReminder.AppService.Auth.DTOs;

namespace HealthReminder.AppService.Interfaces.Auth
{
    public interface IAuthAppService
    {
        Task<string> LoginAsync(LoginUserDto loginUserDto);
        Task RegisterAsync(RegisterUserDto registerUserDto);
    }
}