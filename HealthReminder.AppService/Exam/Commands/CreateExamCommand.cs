using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthReminder.AppService.Exam.Commands
{
    public class CreateExamCommand
    {
        public string Name { get; set; }
        public DateTime ScheduledDate { get; set; }
        public DateTime SeekExamDate { get; set; }
        public Guid UserId { get; set; }
        public Guid MedicalAppointmentId { get; set; }
    }
}
