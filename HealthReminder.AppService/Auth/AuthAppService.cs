using HealthReminder.AppService.Interfaces.Auth;
using HealthReminder.AppService.Auth.DTOs;
using HealthReminder.Domain.Users;
using HealthReminder.Domain.Users.Repositories;

namespace HealthReminder.AppService.Users
{
    public class AuthAppService : IAuthAppService
    {
        private readonly IAuthRepository _authRepository;

        public AuthAppService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task RegisterAsync(RegisterUserDto registerUserDto)
        {
            if (registerUserDto == null) throw new ArgumentNullException(nameof(registerUserDto));

            var existingUser = await _authRepository.GetUserByEmailAsync(registerUserDto.Email);
            if (existingUser != null) throw new ArgumentException("Email já registrado");

            var user = new User(registerUserDto.Name, registerUserDto.Email, registerUserDto.Password);
            await _authRepository.AddUserAsync(user);
        }

        public async Task<User> LoginAsync(LoginUserDto loginUserDto)
        {
            if (loginUserDto == null) throw new ArgumentNullException(nameof(loginUserDto));

            var user = await _authRepository.GetUserByEmailAsync(loginUserDto.Email);
            if (user == null || !user.VerifyPassword(loginUserDto.Password)) throw new UnauthorizedAccessException("Email ou senha inválidos.");

            return user;
        }
    }
}