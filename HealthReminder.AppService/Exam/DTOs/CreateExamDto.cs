using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthReminder.AppService.Exam.DTOs
{
    public class CreateExamDto
    {
        [Required(ErrorMessage = "Nome do exame é obrigatório.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Data que o exame foi feito é obrigatório.")]
        public DateTime ScheduledDate { get; set; }

        [Required(ErrorMessage = "Data de busca do exame é obrigatório.")]
        public DateTime SeekExam { get; set; }
    }
}
