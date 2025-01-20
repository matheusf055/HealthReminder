using HealthReminder.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthReminder.AppService.Interfaces
{
    public interface ITokenAppService
    {
        string GenerateToken(User user);
    }
}
