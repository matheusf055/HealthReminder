using HealthReminder.Domain.Common.Auditable;
using HealthReminder.Domain.Common.Entities;
using HealthReminder.Domain.Users;
using System;

namespace HealthReminder.Domain.Medications
{
    public class Medication : EntityBase, ICreateAuditable
    {
        public Medication() { }

        public Medication(string name, string dosage, string frequency, int totalPills, Guid userId, Guid createUserId, string createUser)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name), "Digite o nome do medicamento");
            Dosage = dosage ?? throw new ArgumentNullException(nameof(dosage), "Digite a dosagem do medicamento");
            Frequency = frequency ?? throw new ArgumentNullException(nameof(frequency), "Digite a frequência do medicamento");

            TotalPills = totalPills;
            AlertThreshold = totalPills * 0.2;

            CreateUserId = createUserId;
            CreateUser = createUser;
            CreateDate = DateTime.UtcNow;

            UserId = userId;
            IsLowStockAlertSent = ShouldAlertForRefill();
        }

        public string Name { get; set; }
        public string Dosage { get; set; }
        public string Frequency { get; set; }
        public int TotalPills { get; set; }
        public double AlertThreshold { get; set; }
        public bool IsLowStockAlertSent { get; set; }
        public Guid UserId { get; set; }

        #region auditable
        public Guid CreateUserId { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        #endregion

        private bool ShouldAlertForRefill()
        {
            return TotalPills <= AlertThreshold;
        }

        public void TakePill()
        {
            if (TotalPills > 0)
            {
                TotalPills--;
                IsLowStockAlertSent = ShouldAlertForRefill();
            } 
            else 
            {
                throw new InvalidOperationException("Não há mais pílulas disponíveis.");
            }
        }
    }
}