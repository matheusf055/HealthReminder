using HealthReminder.AppService.User.DTOs;
using HealthReminder.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthReminder.AppService.Interfaces.User
{
    public interface IUserAppService
    {
        Task<UserDetailsDto> GetById(Guid userId, IUser user);
        Task Update(UpdateUserDto updateUserDto, Guid userId, IUser user);
        Task Delete(Guid userId, IUser user);
    }
}
