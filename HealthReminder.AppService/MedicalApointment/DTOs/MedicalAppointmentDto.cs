using HealthReminder.AppService.Exam.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthReminder.AppService.MedicalApointment.DTOs
{
    public class MedicalAppointmentDto
    {
        public Guid Id { get; set; }
        public string DoctorName { get; set; }
        public string? Specialty { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public string Location { get; set; }
        public Guid UserId { get; set; }
        public List<ExamDto> Exams { get; set; } = new List<ExamDto>();
    }
}
