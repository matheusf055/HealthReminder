using HealthReminder.AppService.MedicalApointment.Commands;
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
        Task<CreateMedicalAppointmentDto> Create(CreateMedicalAppointmentCommand command, IUser user);
        Task<MedicalAppointmentDto> GetById(Guid userId, Guid appointmentId, IUser user);
        Task<List<MedicalAppointmentDto>> GetAll(Guid userId, IUser user);
        Task Update(Guid userId, Guid appointmentId, UpdateMedicalAppointmentDto updateMedicalAppointmentDto, IUser user);
        Task Delete(Guid userId, Guid appointmentId, IUser user);
    }
}
