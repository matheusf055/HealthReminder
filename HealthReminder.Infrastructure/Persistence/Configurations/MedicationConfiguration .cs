using HealthReminder.Domain.Medication;
using HealthReminder.Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthReminder.Infrastructure.Persistence.Configurations
{
    public class MedicationConfiguration : IEntityTypeConfiguration<Medications>
    {
        public void Configure(EntityTypeBuilder<Medications> builder)
        {
            builder.ToTable("Medications")
                .HasKey(m => m.Id); 

            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(m => m.Dosage)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(m => m.Frequency)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(m => m.TotalPills)
                .IsRequired();

            builder.Property(m => m.AlertThreshold)
                .IsRequired();

            builder.Property(m => m.IsLowStockAlertSent)
                .IsRequired();

            builder.Property(m => m.CreateUserId)
                .IsRequired();

            builder.Property(m => m.CreateUser)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(m => m.CreateDate)
                .IsRequired();

            builder.HasOne<Users>()
               .WithMany()
               .HasForeignKey(m => m.UserId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Users>()
               .WithMany()
               .HasForeignKey(m => m.CreateUserId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
