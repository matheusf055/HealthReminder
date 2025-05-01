using HealthReminder.Domain.Common.Auditable;
using HealthReminder.Domain.Common.Entities;
using HealthReminder.Domain.Exam;

namespace HealthReminder.Domain.MedicalAppointment
{
    public class MedicalAppointments : EntityBase, ICreateAuditable
    {
        public MedicalAppointments() { }

        public MedicalAppointments(string doctorName, string? specialty, DateTime appointmentDateTime, string location, Guid userId)
        {
            DoctorName = doctorName;
            Specialty = specialty;
            AppointmentDateTime = appointmentDateTime;
            Location = location;

            UserId = userId;
        }

        public string DoctorName { get; set; }
        public string? Specialty { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public string Location { get; set; }
        public Guid UserId { get; set; }
        public ICollection<Exams> Exams { get; set; } = new List<Exams>();

        #region auditable
        public Guid CreateUserId { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        #endregion
    }
}