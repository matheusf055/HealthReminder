using HealthReminder.Domain.Users;
using HealthReminder.Domain.Users.Repositories;
using HealthReminder.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthReminder.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly HealthReminderDbContext _context;
        public UserRepository(HealthReminderDbContext context)
        {
            _context = context;
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Set<User>().FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
