using HealthReminder.Domain.Common.Auditable;
using HealthReminder.Domain.Common.Entities;
using HealthReminder.Domain.Users;
using System;

namespace HealthReminder.Domain.MedicalAppointments
{
    public class MedicalAppointment : EntityBase, IUpdateAuditable, ICreateAuditable
    {
        public MedicalAppointment(string doctorName, string specialty, DateTime appointmentDateTime, string location, Guid createUserId, string createUser)
        {
            DoctorName = doctorName ?? throw new ArgumentNullException(nameof(doctorName), "Digite o nome do médico");
            Specialty = specialty;
            AppointmentDateTime = appointmentDateTime;
            Location = location ?? throw new ArgumentNullException(nameof(location), "Digite o local da consulta");
            CreateUserId = createUserId;
            CreateUser = createUser ?? throw new ArgumentNullException(nameof(createUser), "Digite o nome do usuário que criou");
            CreateDate = DateTime.UtcNow;
        }

        public string DoctorName { get; set; }
        public string? Specialty { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public string Location { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }

        #region auditable
        public Guid CreateUserId { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid? UpdateUserId { get; set; }
        public string? UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        #endregion
    }
}