using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthReminder.AppService.Medication.DTOs
{
    public class UpdateMedicationDto
    {
        public string Name { get; set; }
        public string Dosage { get; set; }
        public string Frequency { get; set; }
        public int TotalPills { get; set; }
        public double AlertThreshold { get; set; }
        public bool IsLowStockAlertSent { get; set; }
    }
}
