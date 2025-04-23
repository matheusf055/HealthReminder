using HealthReminder.Domain.MedicalAppointments;
using HealthReminder.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthReminder.Infrastructure.Persistence.Configurations
{
    public class MedicalAppointmentConfiguration : IEntityTypeConfiguration<MedicalAppointments>
    {
        public void Configure(EntityTypeBuilder<MedicalAppointments> builder)
        {
            builder.ToTable("MedicalAppointments")
                .HasKey(ma => ma.Id); 

            builder.Property(ma => ma.DoctorName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(ma => ma.Specialty)
                .HasMaxLength(255); 

            builder.Property(ma => ma.AppointmentDateTime)
                .IsRequired();

            builder.Property(ma => ma.Location)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(ma => ma.CreateUserId)
                .IsRequired();

            builder.Property(ma => ma.CreateUser)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(ma => ma.CreateDate)
                .IsRequired();

            builder.HasMany(ma => ma.Exams)  
                .WithOne(e => e.MedicalAppointment)  
                .HasForeignKey(e => e.MedicalAppointmentId)  
                .OnDelete(DeleteBehavior.SetNull);  

            builder.HasOne<Users>()
                .WithMany()
                .HasForeignKey(ma => ma.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Users>()
               .WithMany()
               .HasForeignKey(ma => ma.CreateUserId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
