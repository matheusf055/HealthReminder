using HealthReminder.AppService.Interfaces.Auth;
using HealthReminder.AppService.Auth.DTOs;
using HealthReminder.Domain.Users.Repositories;

namespace HealthReminder.AppService.Users
{
    public class AuthAppService : IAuthAppService
    {
        private readonly IUserRepository _userRepository;

        public AuthAppService(IUserRepository authRepository)
        {
            _userRepository = authRepository;
        }

        public async Task RegisterAsync(RegisterUserDto registerUserDto)
        {
            if (registerUserDto == null) throw new ArgumentNullException(nameof(registerUserDto));

            var existingUser = await _userRepository.GetUserByEmailAsync(registerUserDto.Email);
            if (existingUser != null) throw new ArgumentException("Email já registrado");

            var user = new Domain.Users.User(registerUserDto.Name, registerUserDto.Email, registerUserDto.Password);
            await _userRepository.AddUserAsync(user);
        }

        public async Task<Domain.Users.User> LoginAsync(LoginUserDto loginUserDto)
        {
            if (loginUserDto == null) throw new ArgumentNullException(nameof(loginUserDto));

            var user = await _userRepository.GetUserByEmailAsync(loginUserDto.Email);
            if (user == null || !user.VerifyPassword(loginUserDto.Password)) throw new UnauthorizedAccessException("Email ou senha inválidos.");

            return user;
        }
    }
}