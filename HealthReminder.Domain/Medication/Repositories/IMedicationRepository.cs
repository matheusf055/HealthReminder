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
        Task Create(Medications medication, IUser user);
        Task<Medications> GetById(Guid id, Guid userId);
        Task<List<Medications>> GetAll(Guid userId);
        Task Update(Medications medication);
        Task Delete(Guid id, Guid userId);
    }
}
