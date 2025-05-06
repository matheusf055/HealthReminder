using HealthReminder.AppService.Medication.Commands;
using HealthReminder.AppService.Medication.DTOs;
using HealthReminder.Domain.Common;
using System;

namespace HealthReminder.AppService.Interfaces.Medication
{
    public interface IMedicationAppService
    {
        Task<MedicationDto> Create(CreateMedicationCommand command, IUser user);
        Task Take(Guid medicationId, IUser user);
        Task<MedicationDto> GetById(Guid medicationId, IUser user);
        Task<List<MedicationDto>> GetAll(IUser user);
        Task Update(UpdateMedicationCommand command, IUser user);
        Task Delete(Guid medicationId, IUser user);
    }
}
