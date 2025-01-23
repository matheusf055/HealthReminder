using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthReminder.Domain.MedicalAppointments.Repositories
{
    public interface IMedicalAppointmentRepository
    {
        Task AddMedicalAppointmentAsync(MedicalAppointment medicalAppointment);
        Task<MedicalAppointment> GetMedicalAppointmentByIdAsync(Guid id, Guid userId);
        Task<List<MedicalAppointment>> GetMedicalAppointmentsByUserIdAsync(Guid userId);
        Task UpdateMedicalAppointmentAsync(MedicalAppointment medicalAppointment);
        Task DeleteMedicalAppointmentAsync(Guid id, Guid userId);
    }
}
