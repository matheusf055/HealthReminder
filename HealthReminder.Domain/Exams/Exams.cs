using HealthReminder.Domain.Common.Auditable;
using HealthReminder.Domain.Common.Entities;
using HealthReminder.Domain.MedicalAppointments;

namespace HealthReminder.Domain.Exams
{
    public class Exams : EntityBase, ICreateAuditable
    {
        public Exams() { }

        public Exams(string name, DateTime scheduledDate, DateTime seekExamDate, Guid userId, Guid? medicalAppointmentId, Guid createUserId, string createUser)
        {
            Name = name;
            ScheduledDate = scheduledDate;
            SeekExamDate = seekExamDate;

            UserId = userId;
            MedicalAppointmentId = medicalAppointmentId;

            CreateUserId = createUserId;
            CreateUser = createUser;
            CreateDate = DateTime.UtcNow;
        }

        public string Name { get; set; }
        public DateTime ScheduledDate { get; set; }
        public DateTime SeekExamDate { get; set; }
        public Guid UserId { get; set; }

        public Guid? MedicalAppointmentId { get; set; }
        public MedicalAppointments.MedicalAppointments? MedicalAppointment { get; set; }

        #region auditable
        public Guid CreateUserId { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        #endregion
    }
}