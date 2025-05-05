using HealthReminder.AppService.Auth;
using HealthReminder.AppService.Exam;
using HealthReminder.AppService.Interfaces.Auth;
using HealthReminder.AppService.Interfaces.Exam;
using HealthReminder.AppService.Interfaces.MedicalAppointment;
using HealthReminder.AppService.Interfaces.Medication;
using HealthReminder.AppService.Interfaces.User;
using HealthReminder.AppService.MedicalApointment;
using HealthReminder.AppService.Medication;
using HealthReminder.AppService.User;
using HealthReminder.Domain.Common;
using HealthReminder.Domain.Common.Security;
using HealthReminder.Domain.Exam.Repositories;
using HealthReminder.Domain.MedicalAppointment.Repositories;
using HealthReminder.Domain.Medication.Repositories;
using HealthReminder.Domain.User.Repositories;
using HealthReminder.Infrastructure.Persistence;
using HealthReminder.Infrastructure.Repositories.Exam;
using HealthReminder.Infrastructure.Repositories.MedicalAppointment;
using HealthReminder.Infrastructure.Repositories.Medication;
using HealthReminder.Infrastructure.Repositories.User;
using HealthReminder.Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HealthReminder.Api.DI
{
    public class DependencyInjection
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<HealthReminderDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUserAppService, UserAppService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthAppService, AuthAppService>();
            services.AddScoped<IMedicationAppService, MedicationAppService>();
            services.AddScoped<IMedicationRepository, MedicationRepository>();
            services.AddScoped<IMedicalAppointmentAppService, MedicalApointmentAppService>();
            services.AddScoped<IMedicalAppointmentRepository, MedicalAppointmentRepository>();
            services.AddScoped<IExamAppService, ExamAppService>();
            services.AddScoped<IExamRepository, ExamRepository>();
            services.AddScoped<IJwtTokenProvider, JwtTokenProvider>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Jwt:Key"]))
                    };
                });

            services.AddScoped<IUser>(provider =>
            {
                var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();
                var user = httpContextAccessor.HttpContext?.User;
                if (user == null)
                {
                    throw new UnauthorizedAccessException("Usuário não autenticado.");
                }
                return new UserToken(user);
            });

            services.AddHttpContextAccessor();
        }
    }
}
