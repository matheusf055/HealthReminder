using HealthReminder.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthReminder.Domain.Common.Security
{
    public interface IJwtTokenProvider
    {
        string GenerateToken(Domain.Users.Users user);
    }
}
