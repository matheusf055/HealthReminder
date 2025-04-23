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
        Task AddMedicalAppointmentAsync(Guid userId, CreateMedicalAppointmentDto createMedicalAppointmentDto, IUser user);
        Task<MedicalAppointmentDto> GetMedicalAppointmentByIdAsync(Guid userId, Guid appointmentId, IUser user);
        Task<List<MedicalAppointmentDto>> GetMedicalAppointmentsByUserIdAsync(Guid userId, IUser user);
        Task UpdateMedicalAppointmentAsync(Guid userId, Guid appointmentId, UpdateMedicalAppointmentDto updateMedicalAppointmentDto, IUser user);
        Task DeleteMedicalAppointmentAsync(Guid userId, Guid appointmentId, IUser user);
    }
}
