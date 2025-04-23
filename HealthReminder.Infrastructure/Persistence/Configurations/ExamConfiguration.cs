using HealthReminder.Domain.Exams;
using HealthReminder.Domain.MedicalAppointments;
using HealthReminder.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthReminder.Infrastructure.Persistence.Configurations
{
    public class ExamConfiguration : IEntityTypeConfiguration<Exams>
    {
        public void Configure(EntityTypeBuilder<Exams> builder)
        {
            builder.ToTable("Exams")  
                .HasKey(e => e.Id);  

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.ScheduledDate)
                .IsRequired();

            builder.Property(e => e.SeekExamDate)
                .IsRequired();

            builder.Property(e => e.UserId)
                .IsRequired();

            builder.Property(e => e.CreateUserId)
                .IsRequired();

            builder.Property(e => e.CreateUser)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.CreateDate)
                .IsRequired();
            
            builder.HasOne(e => e.MedicalAppointment)  
                .WithMany(ma => ma.Exams)  
                .HasForeignKey(e => e.MedicalAppointmentId)  
                .OnDelete(DeleteBehavior.SetNull);  

            builder.HasOne<Users>()
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Users>()
                .WithMany()
                .HasForeignKey(e => e.CreateUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
