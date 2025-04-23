using HealthReminder.AppService.Interfaces.Auth;
using HealthReminder.AppService.Auth.DTOs;
using Microsoft.AspNetCore.Mvc;
using HealthReminder.Domain.Common.Security;

namespace HealthReminder.Api.Controllers.Auth
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthAppService _authAppService;

        public AuthController(IAuthAppService authService)
        {
            _authAppService = authService;
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
            var token = await _authAppService.LoginAsync(loginUserDto);
            return Ok(new { token });
        }
    }
}
