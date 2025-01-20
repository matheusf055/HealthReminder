using HealthReminder.Domain.Medications;
using HealthReminder.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthReminder.Infrastructure.Persistence.Configurations
{
    public class MedicationConfiguration : IEntityTypeConfiguration<Medication>
    {
        public void Configure(EntityTypeBuilder<Medication> builder)
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

            builder.Property(m => m.UpdateUserId);

            builder.Property(m => m.UpdateUser)
                .HasMaxLength(255);

            builder.Property(m => m.UpdateDate);

            builder.HasOne<User>()
               .WithMany()
               .HasForeignKey(m => m.CreateUserId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
