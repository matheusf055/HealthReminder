using HealthReminder.AppService.Interfaces.Auth;
using HealthReminder.AppService.Interfaces.MedicalAppointment;
using HealthReminder.AppService.Interfaces.Medication;
using HealthReminder.AppService.MedicalApointment;
using HealthReminder.AppService.Medication;
using HealthReminder.AppService.Users;
using HealthReminder.Domain.Common;
using HealthReminder.Domain.MedicalAppointments.Repositories;
using HealthReminder.Domain.Medications.Repositories;
using HealthReminder.Domain.Users.Repositories;
using HealthReminder.Infrastructure.Persistence;
using HealthReminder.Infrastructure.Repositories.MedicalAppointment;
using HealthReminder.Infrastructure.Repositories.Medication;
using HealthReminder.Infrastructure.Repositories.User;
using Microsoft.EntityFrameworkCore;

namespace HealthReminder.Api.DI
{
    public class DependencyInjection
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<HealthReminderDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthAppService, AuthAppService>();
            services.AddScoped<IMedicationAppService, MedicationAppService>();
            services.AddScoped<IMedicationRepository, MedicationRepository>();
            services.AddScoped<IMedicalAppointmentAppService, MedicalApointmentAppService>();
            services.AddScoped<IMedicalAppointmentRepository, MedicalAppointmentRepository>();
            services.AddScoped<ITokenAppService, TokenAppService>();

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
