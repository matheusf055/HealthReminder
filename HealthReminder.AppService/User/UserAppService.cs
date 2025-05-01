using HealthReminder.AppService.Interfaces.User;
using HealthReminder.AppService.User.DTOs;
using HealthReminder.Domain.Common;
using HealthReminder.Domain.User.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthReminder.AppService.User
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserRepository _userRepository;

        public UserAppService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> GetById(Guid userId, IUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var userDetails = await _userRepository.GetUserByEmailAsync(user.Email);
            if (userDetails == null) throw new KeyNotFoundException("Usuário não encontrado");

            return new UserDto
            {
                Name = userDetails.Name,
                Email = userDetails.Email,
                CreateDate = userDetails.CreateDate
            };
        }

        public async Task Delete(Guid userId, IUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var userDetails = await _userRepository.GetUserByIdAsync(userId);
            if (userDetails == null) throw new KeyNotFoundException("Usuário não encontrado");

            await _userRepository.DeleteUserAsync(userId);
        }
    }
}
