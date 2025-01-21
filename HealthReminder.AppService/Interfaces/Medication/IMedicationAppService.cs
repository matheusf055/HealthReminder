using HealthReminder.AppService.Medication.DTOs;
using HealthReminder.Domain.Common;
using HealthReminder.Domain.Users;
using System;

namespace HealthReminder.AppService.Interfaces.Medication
{
    public interface IMedicationAppService
    {
        Task AddMedicationAsync(CreateMedicationDto createMedicationDto, IUser user);
        Task TakeMedicationAsync(Guid id, IUser user);
        Task<MedicationDto> GetMedicationByIdAsync(Guid id, IUser user);
        Task<List<MedicationDto>> GetMedicationsByUserIdAsync(Guid userId, IUser user);
        Task UpdateMedicationAsync(Guid id, UpdateMedicationDto updateMedicationDto, IUser user);
        Task DeleteMedicationAsync(Guid id, IUser user);
    }
}
