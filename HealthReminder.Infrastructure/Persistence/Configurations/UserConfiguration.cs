using HealthReminder.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthReminder.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.ToTable("UsersAccount")
                .HasKey(u => u.Id);

            builder.Property(u => u.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(u => u.Email)
               .IsRequired()
               .HasMaxLength(255);

            builder.HasIndex(u => u.Email)
               .IsUnique()
               .HasDatabaseName("UQ_Email");

            builder.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(u => u.CreateDate)
                .IsRequired();
        }
    }
}
