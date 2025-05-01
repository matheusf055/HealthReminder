using HealthReminder.Domain.Common.Auditable;
using HealthReminder.Domain.Common.Entities;

namespace HealthReminder.Domain.Medication
{
    public class Medications : EntityBase, ICreateAuditable
    {
        public Medications() { }

        public Medications(string name, string dosage, string frequency, int totalPills, Guid userId)
        {
            Name = name;
            Dosage = dosage;
            Frequency = frequency;

            TotalPills = totalPills;
            AlertThreshold = totalPills * 0.2;

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