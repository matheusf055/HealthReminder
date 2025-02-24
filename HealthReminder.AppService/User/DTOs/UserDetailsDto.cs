using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthReminder.AppService.User.DTOs
{
    public class UserDetailsDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
