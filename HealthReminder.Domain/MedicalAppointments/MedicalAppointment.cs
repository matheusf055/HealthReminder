using HealthReminder.Domain.Common.Auditable;
using HealthReminder.Domain.Common.Entities;
using HealthReminder.Domain.Exams;

namespace HealthReminder.Domain.MedicalAppointments
{
    public class MedicalAppointment : EntityBase, ICreateAuditable
    {
        public MedicalAppointment() { }

        public MedicalAppointment(string doctorName, string specialty, DateTime appointmentDateTime, string location, Guid userId, Guid createUserId, string createUser)
        {
            DoctorName = doctorName;
            Specialty = specialty;
            AppointmentDateTime = appointmentDateTime;
            Location = location;

            UserId = userId;

            CreateUserId = createUserId;
            CreateUser = createUser;
            CreateDate = DateTime.UtcNow;
        }

        public string DoctorName { get; set; }
        public string? Specialty { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public string Location { get; set; }
        public Guid UserId { get; set; }
        public ICollection<Exam> Exams { get; set; } = new List<Exam>();

        #region auditable
        public Guid CreateUserId { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        #endregion
    }
}