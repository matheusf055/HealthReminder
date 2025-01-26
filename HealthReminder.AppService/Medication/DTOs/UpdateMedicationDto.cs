using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthReminder.AppService.Medication.DTOs
{
    public class UpdateMedicationDto
    {
        [Required(ErrorMessage = "Nome é obrigatório.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Dosagem é obrigatória.")]
        public string Dosage { get; set; }

        [Required(ErrorMessage = "Frequência é obrigatória.")]
        public string Frequency { get; set; }

        [Required(ErrorMessage = "Total de pílulas é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "Total de pílulas deve ser maior que zero.")]
        public int TotalPills { get; set; }

        public double AlertThreshold { get; set; }

        public bool IsLowStockAlertSent { get; set; }
    }
}
