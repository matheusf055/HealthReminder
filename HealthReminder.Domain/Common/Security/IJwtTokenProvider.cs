using HealthReminder.Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthReminder.Domain.Common.Security
{
    public interface IJwtTokenProvider
    {
        string GenerateToken(Users user);
    }
}
