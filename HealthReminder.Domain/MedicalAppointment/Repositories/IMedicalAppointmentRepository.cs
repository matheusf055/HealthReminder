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
        Task Create(MedicalAppointments medicalAppointment, IUser user);
        Task<MedicalAppointments> GetById(Guid id, Guid userId);
        Task<List<MedicalAppointments>> GetAll(Guid userId);
        Task Update(MedicalAppointments medicalAppointment);
        Task Delete(Guid id, Guid userId);
    }
}
