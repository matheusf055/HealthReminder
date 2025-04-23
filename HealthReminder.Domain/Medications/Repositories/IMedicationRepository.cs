using HealthReminder.Domain.Common;
using HealthReminder.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthReminder.Domain.Medications.Repositories
{
    public interface IMedicationRepository
    {
        Task AddMedicationAsync(Medications medication);
        Task<Medications> GetMedicationByIdAsync(Guid id, Guid userId);
        Task<List<Medications>> GetMedicationsByUserIdAsync(Guid userId);
        Task UpdateMedicationAsync(Medications medication);
        Task DeleteMedicationAsync(Guid id, Guid userId);
    }
}
