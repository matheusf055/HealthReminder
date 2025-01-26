using HealthReminder.AppService.Interfaces.Auth;
using HealthReminder.AppService.Auth.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HealthReminder.Api.Controllers.Auth
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthAppService _authAppService;
        private readonly ITokenAppService _tokenAppService;

        public AuthController(IAuthAppService authService, ITokenAppService tokenAppService)
        {
            _authAppService = authService;
            _tokenAppService = tokenAppService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
        {
            await _authAppService.RegisterAsync(registerUserDto);
            return Ok("Usuário registrado com sucesso.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto loginUserDto)
        {
            var user = await _authAppService.LoginAsync(loginUserDto);
            var token = _tokenAppService.GenerateToken(user);
            return Ok(new { Token = token });
        }
    }
}
