using HealthReminder.AppService.MedicalApointment.DTOs;
using HealthReminder.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthReminder.AppService.Interfaces.MedicalAppointment
{
    public interface IMedicalAppointmentAppService
    {
        Task AddMedicalAppointmentAsync(CreateMedicalAppointmentDto createMedicalAppointmentDto, IUser user);
        Task<MedicalAppointmentDto> GetMedicalAppointmentByIdAsync(Guid id, IUser user);
        Task<List<MedicalAppointmentDto>> GetMedicalAppointmentsByUserIdAsync(Guid userId, IUser user);
        Task UpdateMedicalAppointmentAsync(Guid id, UpdateMedicalAppointmentDto updateMedicalAppointmentDto, IUser user);
        Task DeleteMedicalAppointmentAsync(Guid id, IUser user);
    }
}
