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
        Task<MedicalAppointmentDto> Create(CreateMedicalAppointmentCommand command, IUser user);
        Task<MedicalAppointmentDto> GetById(Guid appointmentId, IUser user);
        Task<List<MedicalAppointmentDto>> GetAll(IUser user);
        Task Update(UpdateMedicalAppointmentCommand command, IUser user);
        Task Delete(Guid appointmentId, IUser user);
    }
}
