using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthReminder.AppService.MedicalApointment.DTOs
{
    public class CreateMedicalAppointmentDto
    {
        [Required(ErrorMessage = "Nome do médico é obrigatório.")]
        public string DoctorName { get; set; }

        public string? Specialty { get; set; }

        [Required(ErrorMessage = "Data e hora da consulta são obrigatórias.")]
        public DateTime AppointmentDateTime { get; set; }

        [Required(ErrorMessage = "Localização é obrigatória.")]
        public string Location { get; set; }
    }
}
