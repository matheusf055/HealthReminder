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
        Task AddMedicationAsync(Medication medication);
        Task<Medication> GetMedicationByIdAsync(Guid id, Guid userId);
        Task<List<Medication>> GetMedicationsByUserIdAsync(Guid userId);
        Task UpdateMedicationAsync(Medication medication);
        Task DeleteMedicationAsync(Guid id, Guid userId);
    }
}
