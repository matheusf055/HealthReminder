using HealthReminder.AppService.Interfaces;
using HealthReminder.AppService.Users.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HealthReminder.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthAppService _authService;

        public AuthController(IAuthAppService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
        {
            try
            {
                await _authService.RegisterAsync(registerUserDto);
                return Ok("Usuário registrado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto loginUserDto)
        {
            try
            {
                var user = await _authService.LoginAsync(loginUserDto);
                return Ok(user); //TODO Implementar JWT
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
