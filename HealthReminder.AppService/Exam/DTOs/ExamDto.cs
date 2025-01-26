using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthReminder.AppService.Exam.DTOs
{
    public class ExamDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime ScheduledDate { get; set; }
        public DateTime SeekExam { get; set; }
        public Guid UserId { get; set; }
    }
}
