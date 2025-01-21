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
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email); 
        }
    }
}
