using HealthReminder.AppService.Interfaces.Auth;
using HealthReminder.AppService.Auth.DTOs;
using Microsoft.AspNetCore.Mvc;
using HealthReminder.Domain.Common.Security;
using Swashbuckle.AspNetCore.Annotations;

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
        [SwaggerOperation(
            Summary = "Registra um novo usuário",
            Description = "Cria uma nova conta de usuário no sistema"
        )]
        [SwaggerResponse(200, "Usuário registrado com sucesso")]
        [SwaggerResponse(400, "Dados inválidos fornecidos")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
        {
            await _authAppService.Register(registerUserDto);
            return Ok("Usuário registrado com sucesso.");
        }

        [HttpPost("login")]
        [SwaggerOperation(
            Summary = "Autentica um usuário",
            Description = "Realiza o login do usuário e retorna um token JWT"
        )]
        [SwaggerResponse(200, "Login realizado com sucesso", typeof(object))]
        [SwaggerResponse(401, "Credenciais inválidas")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login(LoginUserDto loginUserDto)
        {
            var token = await _authAppService.Login(loginUserDto);
            return Ok(new { token });
        }
    }
}
