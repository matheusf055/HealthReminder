using HealthReminder.AppService.Interfaces;
using HealthReminder.AppService.Users;
using HealthReminder.Domain.Users.Repositories;
using HealthReminder.Infrastructure.Persistence;
using HealthReminder.Infrastructure.Repositories;
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
        }
    }
}
