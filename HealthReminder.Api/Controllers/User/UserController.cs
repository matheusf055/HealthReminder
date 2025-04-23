using HealthReminder.AppService.Interfaces.User;
using HealthReminder.AppService.User.DTOs;
using HealthReminder.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetUserDetails([FromRoute] Guid userId)
        {
            var user = await _userAppService.GetUserDetails(userId, _user);
            return Ok(user);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUserAsync([FromRoute] Guid userId, [FromBody] UpdateUserDto updateUserDto)
        {
            await _userAppService.UpdateUserAsync(updateUserDto, userId, _user);
            return Ok("Usuário atualizado com sucesso.");
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUserAsync([FromRoute] Guid userId)
        {
            await _userAppService.DeleteUserAsync(userId, _user);
            return Ok("Usuário deletado com sucesso.");
        }
    }
}
