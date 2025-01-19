using HealthReminder.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using RememberMedication.Domain.Users;

namespace HealthReminder.Infrastructure.Persistence
{
    public class HealthReminderDbContext : DbContext
    {
        public HealthReminderDbContext(DbContextOptions<HealthReminderDbContext> options) 
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
