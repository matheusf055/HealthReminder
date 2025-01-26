using HealthReminder.Domain.Common.Auditable;
using HealthReminder.Domain.Common.Entities;
using HealthReminder.Domain.MedicalAppointments;

namespace HealthReminder.Domain.Exams
{
    public class Exam : EntityBase, ICreateAuditable
    {
        public Exam() { }

        public Exam(string name, DateTime scheduledDate, DateTime seekExam, Guid userId, Guid createUserId, string createUser)
        {
            Name = name;
            ScheduledDate = scheduledDate;
            SeekExam = seekExam;
            UserId = userId;

            CreateUserId = createUserId;
            CreateUser = createUser;
            CreateDate = DateTime.UtcNow;
        }

        public string Name { get; set; }
        public DateTime ScheduledDate { get; set; }
        public DateTime SeekExam { get; set; }
        public Guid UserId { get; set; }

        public Guid? MedicalAppointmentId { get; set; }
        public MedicalAppointment? MedicalAppointment { get; set; }

        #region auditable
        public Guid CreateUserId { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        #endregion
    }
}