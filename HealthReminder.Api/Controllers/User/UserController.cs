using HealthReminder.AppService.Interfaces.User;
using HealthReminder.AppService.User.DTOs;
using HealthReminder.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthReminder.Api.Controllers.User
{
    [ApiController]
    [Route("api/user")]
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

        [HttpGet("details")]
        public async Task<IActionResult> GetUserDetails()
        {
            var user = await _userAppService.GetUserDetails(_user.Id, _user);
            return Ok(user);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UpdateUserDto updateUserDto)
        {
            await _userAppService.UpdateUserAsync(updateUserDto, _user);
            return Ok("Usuário atualizado com sucesso.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync(Guid id)
        {
            await _userAppService.DeleteUserAsync(id, _user);
            return Ok("Usuário deletado com sucesso.");
        }
    }
}
