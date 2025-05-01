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

        [HttpGet("{userId}")]
        [SwaggerOperation(
            Summary = "Obtém detalhes do usuário",
            Description = "Retorna os detalhes de um usuário específico pelo ID"
        )]
        [SwaggerResponse(200, "Detalhes do usuário retornados com sucesso")]
        [SwaggerResponse(401, "Não autorizado")]
        [SwaggerResponse(404, "Usuário não encontrado")]
        [ProducesResponseType(typeof(UserDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserDetails([FromRoute] Guid userId)
        {
            var user = await _userAppService.GetById(userId, _user);
            return Ok(user);
        }

        [HttpPut("{userId}")]
        [SwaggerOperation(
            Summary = "Atualiza dados do usuário",
            Description = "Atualiza as informações de um usuário existente"
        )]
        [SwaggerResponse(200, "Usuário atualizado com sucesso")]
        [SwaggerResponse(400, "Dados inválidos fornecidos")]
        [SwaggerResponse(401, "Não autorizado")]
        [SwaggerResponse(404, "Usuário não encontrado")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUserAsync([FromRoute] Guid userId, [FromBody] UpdateUserDto updateUserDto)
        {
            await _userAppService.Update(updateUserDto, userId, _user);
            return Ok("Usuário atualizado com sucesso.");
        }

        [HttpDelete("{userId}")]
        [SwaggerOperation(
            Summary = "Remove um usuário",
            Description = "Deleta um usuário do sistema"
        )]
        [SwaggerResponse(200, "Usuário deletado com sucesso")]
        [SwaggerResponse(401, "Não autorizado")]
        [SwaggerResponse(404, "Usuário não encontrado")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUserAsync([FromRoute] Guid userId)
        {
            await _userAppService.Delete(userId, _user);
            return Ok("Usuário deletado com sucesso.");
        }
    }
}
