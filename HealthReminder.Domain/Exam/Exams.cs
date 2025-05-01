using HealthReminder.Domain.Common.Auditable;
using HealthReminder.Domain.Common.Entities;
using HealthReminder.Domain.MedicalAppointment;

namespace HealthReminder.Domain.Exam
{
    public class Exams : EntityBase, ICreateAuditable
    {
        public Exams() { }

        public Exams(string name, DateTime scheduledDate, DateTime seekExamDate, Guid userId, Guid? medicalAppointmentId)
        {
            Name = name;
            ScheduledDate = scheduledDate;
            SeekExamDate = seekExamDate;

            UserId = userId;
            MedicalAppointmentId = medicalAppointmentId;
        }

        public string Name { get; set; }
        public DateTime ScheduledDate { get; set; }
        public DateTime SeekExamDate { get; set; }
        public Guid UserId { get; set; }

        public Guid? MedicalAppointmentId { get; set; }
        public MedicalAppointments? MedicalAppointment { get; set; }

        #region auditable
        public Guid CreateUserId { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        #endregion
    }
}