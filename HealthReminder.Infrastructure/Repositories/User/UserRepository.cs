using HealthReminder.Domain.User;
using HealthReminder.Domain.User.Repositories;
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

        public async Task Create(Users user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<Users> GetById(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Users> GetByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email); 
        }

        public async Task Delete(Guid id)
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
