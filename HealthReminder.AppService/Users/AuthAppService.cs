using HealthReminder.AppService.Interfaces;
using HealthReminder.AppService.Users.DTOs;
using HealthReminder.Domain.Users;
using HealthReminder.Domain.Users.Repositories;

namespace HealthReminder.AppService.Users
{
    public class AuthAppService : IAuthAppService
    {
        private readonly IUserRepository _userRepository;

        public AuthAppService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task RegisterAsync(RegisterUserDto registerUserDto)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(registerUserDto.Email);
            if (existingUser != null)
            {
                throw new Exception("Email já registrado");
            }

            var user = new User(registerUserDto.Name, registerUserDto.Email, registerUserDto.Password, registerUserDto.ConfirmPassword);
            await _userRepository.AddUserAsync(user);
        }

        public async Task<User> LoginAsync(LoginUserDto loginUserDto)
        {
            var user = await _userRepository.GetUserByEmailAsync(loginUserDto.Email);
            if (user == null || !user.VerifyPassword(loginUserDto.Password))
            {
                throw new ArgumentException("Email ou senha inválidos.");
            }

            return user;
        }
    }
}
