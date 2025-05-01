using HealthReminder.AppService.Medication.Commands;
using HealthReminder.AppService.Medication.DTOs;
using HealthReminder.Domain.Common;
using System;

namespace HealthReminder.AppService.Interfaces.Medication
{
    public interface IMedicationAppService
    {
        Task<MedicationDto> Create(CreateMedicationCommand command, IUser user);
        Task Take(Guid userId, Guid medicationId, IUser user);
        Task<MedicationDto> GetById(Guid userId, Guid medicationId, IUser user);
        Task<List<MedicationDto>> GetAll(Guid userId, IUser user);
        Task Update(Guid userId, Guid medicationId, UpdateMedicationDto updateMedicationDto, IUser user);
        Task Delete(Guid userId, Guid medicationId, IUser user);
    }
}
