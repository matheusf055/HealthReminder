using HealthReminder.AppService.Interfaces.Auth;
using HealthReminder.AppService.Auth.DTOs;
using HealthReminder.Domain.Common.Security;
using HealthReminder.Domain.User.Repositories;
using HealthReminder.Domain.User;
using HealthReminder.AppService.Auth.Commands;

namespace HealthReminder.AppService.Auth
{
    public class AuthAppService : IAuthAppService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenProvider _jwtTokenProvider;

        public AuthAppService(IUserRepository authRepository, IPasswordHasher passwordHasher, IJwtTokenProvider jwtTokenProvider)
        {
            _userRepository = authRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenProvider = jwtTokenProvider;
        }

        public async Task Register(RegisterUserCommand command)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));

            var existingUser = await _userRepository.GetByEmail(command.Email);
            if (existingUser != null) throw new ArgumentException("Usuário já registrado");

            if (command.Password != command.ConfirmPassword) throw new ArgumentException("As senhas não coincidem");

            var hashedPassword = _passwordHasher.HashPassword(command.Password);

            var user = new Users(command.Name, command.Email, hashedPassword);
            await _userRepository.Create(user);
        }

        public async Task<string> Login(LoginUserDto loginUserDto)
        {
            if (loginUserDto == null) throw new ArgumentNullException(nameof(loginUserDto));

            var user = await _userRepository.GetByEmail(loginUserDto.Email);
            if (user == null) throw new UnauthorizedAccessException("Email ou senha inválidos.");

            var userPassword = _passwordHasher.VerifyPassword(loginUserDto.Password, user.Password);
            if (userPassword != true) throw new UnauthorizedAccessException("Email ou senha inválidos.");

            var token = _jwtTokenProvider.GenerateToken(user);
            return token;
        }
    }
}