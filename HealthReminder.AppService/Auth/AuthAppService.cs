using HealthReminder.AppService.Interfaces.Auth;
using HealthReminder.AppService.Auth.DTOs;
using HealthReminder.Domain.Common.Security;
using HealthReminder.Domain.User.Repositories;
using HealthReminder.Domain.User;

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

        public async Task Register(RegisterUserDto registerUserDto)
        {
            if (registerUserDto == null) throw new ArgumentNullException(nameof(registerUserDto));

            var existingUser = await _userRepository.GetByEmail(registerUserDto.Email);
            if (existingUser != null) throw new ArgumentException("Usuário já registrado");

            var hashedPassword = _passwordHasher.HashPassword(registerUserDto.Password);

            var user = new Users(registerUserDto.Name, registerUserDto.Email, hashedPassword);
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