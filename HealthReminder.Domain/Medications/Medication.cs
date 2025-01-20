using HealthReminder.Domain.Common.Auditable;
using HealthReminder.Domain.Common.Entities;
using HealthReminder.Domain.Users;
using System;

namespace HealthReminder.Domain.Medications
{
    public class Medication : EntityBase, IUpdateAuditable, ICreateAuditable
    {
        public Medication(string name, string dosage, string frequency, int totalPills, Guid createUserId, string createUser)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name), "Digite o nome do medicamento");
            Dosage = dosage ?? throw new ArgumentNullException(nameof(dosage), "Digite a dosagem do medicamento");
            Frequency = frequency ?? throw new ArgumentNullException(nameof(frequency), "Digite a frequência do medicamento");
            TotalPills = totalPills;
            AlertThreshold = totalPills * 0.2;
            CreateUserId = createUserId;
            CreateUser = createUser ?? throw new ArgumentNullException(nameof(createUser), "Digite o nome do usuário que criou");
            CreateDate = DateTime.UtcNow;
            IsLowStockAlertSent = false;
        }

        public string Name { get; set; }
        public string Dosage { get; set; }
        public string Frequency { get; set; }
        public int TotalPills { get; set; }
        public double AlertThreshold { get; set; }
        public bool IsLowStockAlertSent { get; set; }
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

        public bool ShouldAlertForRefill()
        {
            return TotalPills <= AlertThreshold;
        }
    }
}