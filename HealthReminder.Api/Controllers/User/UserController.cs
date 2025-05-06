using HealthReminder.AppService.Interfaces.User;
using HealthReminder.AppService.User.DTOs;
using HealthReminder.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HealthReminder.Api.Controllers.User
{
    [ApiController]
    [Route("api/users")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserAppService _userAppService;
        private readonly IUser _user;

        public UserController(IUserAppService userAppService, IUser user)
        {
            _userAppService = userAppService;
            _user = user;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Obter usuário por ID",
            Description = "Retorna os dados de um usuário específico"
        )]
        [SwaggerResponse(200, "Usuário encontrado com sucesso", typeof(UserDto))]
        [SwaggerResponse(401, "Não autorizado - Usuário não autenticado")]
        [SwaggerResponse(404, "Não encontrado - Usuário não existe")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById()
        {
            var user = await _userAppService.GetById(_user);
            return Ok(user);
        }

        [HttpDelete]
        [SwaggerOperation(
            Summary = "Excluir consulta médica",
            Description = "Remove uma consulta médica do sistema"
        )]
        [SwaggerResponse(204, "Consulta médica excluída com sucesso")]
        [SwaggerResponse(401, "Não autorizado - Usuário não autenticado")]
        [SwaggerResponse(404, "Não encontrado - Consulta médica não existe")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete()
        {
            await _userAppService.Delete(_user);
            return NoContent();
        }
    }
}
