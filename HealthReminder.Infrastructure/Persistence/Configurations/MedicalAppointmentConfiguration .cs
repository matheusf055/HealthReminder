using HealthReminder.Domain.MedicalAppointments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthReminder.Infrastructure.Persistence.Configurations
{
    public class MedicalAppointmentConfiguration : IEntityTypeConfiguration<MedicalAppointment>
    {
        public void Configure(EntityTypeBuilder<MedicalAppointment> builder)
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

            builder.Property(ma => ma.UpdateUserId);

            builder.Property(ma => ma.UpdateUser)
                .HasMaxLength(255);

            builder.Property(ma => ma.UpdateDate);

            builder.HasOne(ma => ma.User)
                .WithMany()  
                .HasForeignKey(ma => ma.UserId)
                .OnDelete(DeleteBehavior.Restrict);  

            builder.HasOne(ma => ma.User)
                .WithMany()
                .HasForeignKey(ma => ma.CreateUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
