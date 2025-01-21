using HealthReminder.Domain.Common.Auditable;
using HealthReminder.Domain.Common.Entities;
using HealthReminder.Domain.Users;
using System;

namespace HealthReminder.Domain.MedicalAppointments
{
    public class MedicalAppointment : EntityBase, ICreateAuditable
    {
        public MedicalAppointment(string doctorName, string specialty, DateTime appointmentDateTime, string location, Guid createUserId, string createUser)
        {
            DoctorName = doctorName ?? throw new ArgumentNullException(nameof(doctorName), "Digite o nome do médico");
            Specialty = specialty;
            AppointmentDateTime = appointmentDateTime;
            Location = location ?? throw new ArgumentNullException(nameof(location), "Digite o local da consulta");

            CreateUserId = createUserId;
            CreateUser = createUser;
            CreateDate = DateTime.UtcNow;
        }

        public string DoctorName { get; set; }
        public string? Specialty { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public string Location { get; set; }
        public Guid UserId { get; set; }

        #region auditable
        public Guid CreateUserId { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        #endregion
    }
}