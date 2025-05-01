using HealthReminder.AppService.Medication.Commands;
using HealthReminder.AppService.Medication.DTOs;
using HealthReminder.Domain.Common;
using System;

namespace HealthReminder.AppService.Interfaces.Medication
{
    public interface IMedicationAppService
    {
        Task<MedicationDto> AddMedicationAsync(CreateMedicationCommand command, IUser user);
        Task TakeMedicationAsync(Guid userId, Guid medicationId, IUser user);
        Task<MedicationDto> GetMedicationByIdAsync(Guid userId, Guid medicationId, IUser user);
        Task<List<MedicationDto>> GetMedicationsByUserIdAsync(Guid userId, IUser user);
        Task UpdateMedicationAsync(Guid userId, Guid medicationId, UpdateMedicationDto updateMedicationDto, IUser user);
        Task DeleteMedicationAsync(Guid userId, Guid medicationId, IUser user);
    }
}
