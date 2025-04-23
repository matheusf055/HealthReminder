using HealthReminder.Domain.Users;
using HealthReminder.Domain.Users.Repositories;
using HealthReminder.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HealthReminder.Infrastructure.Repositories.User
{
    public class UserRepository : IUserRepository
    {
        private readonly HealthReminderDbContext _context;

        public UserRepository(HealthReminderDbContext context)
        {
            _context = context;
        }

        public async Task AddUserAsync(Domain.Users.User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<Domain.Users.User> GetUserByEmailAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                throw new KeyNotFoundException("Usuário não encontrado.");

            return user;    
        }

        public async Task<Domain.Users.User> GetUserDetailsAsync(Guid id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
                throw new KeyNotFoundException("Usuário não encontrado.");

            return user;
        }

        public async Task UpdateUserAsync(Domain.Users.User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
