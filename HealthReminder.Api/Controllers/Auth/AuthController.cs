using HealthReminder.AppService.Interfaces.Auth;
using HealthReminder.AppService.Auth.DTOs;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using HealthReminder.AppService.Auth.Commands;

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
            Summary = "Registrar novo usuário",
            Description = "Cria uma nova conta de usuário no sistema com as informações fornecidas"
        )]
        [SwaggerResponse(200, "Usuário registrado com sucesso", typeof(string))]
        [SwaggerResponse(400, "Requisição inválida - Dados de registro inválidos ou email já em uso")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(RegisterUserCommand command)
        {
            await _authAppService.Register(command);
            return Ok();
        }

        [HttpPost("login")]
        [SwaggerOperation(
            Summary = "Realizar login do usuário",
            Description = "Autentica o usuário com email e senha, retornando um token JWT para acesso ao sistema"
        )]
        [SwaggerResponse(200, "Login realizado com sucesso - Retorna o token de acesso", typeof(object))]
        [SwaggerResponse(401, "Não autorizado - Credenciais inválidas")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login(LoginUserDto loginUserDto)
        {
            var token = await _authAppService.Login(loginUserDto);
            return Ok(new { token });
        }
    }
}
