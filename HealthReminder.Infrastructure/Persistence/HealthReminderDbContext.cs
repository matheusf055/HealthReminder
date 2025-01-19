using HealthReminder.Domain.MedicalAppointments;
using HealthReminder.Domain.Medications;
using HealthReminder.Domain.Users;
using HealthReminder.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace HealthReminder.Infrastructure.Persistence
{
    public class HealthReminderDbContext : DbContext
    {
        public HealthReminderDbContext(DbContextOptions<HealthReminderDbContext> options) 
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet <MedicalAppointment> MedicalAppointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new MedicationConfiguration());
            modelBuilder.ApplyConfiguration(new MedicalAppointmentConfiguration());
        }
    }
}
