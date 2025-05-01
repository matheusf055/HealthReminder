using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthReminder.Domain.Common;

namespace HealthReminder.Domain.MedicalAppointment.Repositories
{
    public interface IMedicalAppointmentRepository
    {
        Task AddMedicalAppointmentAsync(MedicalAppointments medicalAppointment, IUser user);
        Task<MedicalAppointments> GetMedicalAppointmentByIdAsync(Guid id, Guid userId);
        Task<List<MedicalAppointments>> GetMedicalAppointmentsByUserIdAsync(Guid userId);
        Task UpdateMedicalAppointmentAsync(MedicalAppointments medicalAppointment);
        Task DeleteMedicalAppointmentAsync(Guid id, Guid userId);
    }
}
