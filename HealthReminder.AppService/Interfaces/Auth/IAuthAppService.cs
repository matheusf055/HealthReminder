using System.Threading.Tasks;
using HealthReminder.AppService.Auth.Commands;
using HealthReminder.AppService.Auth.DTOs;

namespace HealthReminder.AppService.Interfaces.Auth
{
    public interface IAuthAppService
    {
        Task<string> Login(LoginUserDto loginUserDto);
        Task Register(RegisterUserCommand command);
    }
}