using HealthReminder.AppService.Interfaces;
using HealthReminder.AppService.Users.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HealthReminder.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            try
            {
                await _authAppService.RegisterAsync(registerUserDto);
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
                var user = await _authAppService.LoginAsync(loginUserDto);
                var token = _tokenAppService.GenerateToken(user);
                return Ok(new {Token = token}); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
