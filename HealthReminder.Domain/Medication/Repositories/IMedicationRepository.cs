using HealthReminder.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthReminder.Domain.Medication.Repositories
{
    public interface IMedicationRepository
    {
        Task AddMedicationAsync(Medications medication, IUser user);
        Task<Medications> GetMedicationByIdAsync(Guid id, Guid userId);
        Task<List<Medications>> GetMedicationsByUserIdAsync(Guid userId);
        Task UpdateMedicationAsync(Medications medication);
        Task DeleteMedicationAsync(Guid id, Guid userId);
    }
}
