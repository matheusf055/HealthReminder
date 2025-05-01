using HealthReminder.Domain.Common;
using HealthReminder.Domain.Medications;
using HealthReminder.Domain.Medications.Repositories;
using HealthReminder.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthReminder.Infrastructure.Repositories.Medication
{
    public class MedicationRepository : IMedicationRepository
    {
        private readonly HealthReminderDbContext _context;

        public MedicationRepository(HealthReminderDbContext context)
        {
            _context = context;
        }

        public async Task AddMedicationAsync(Medications medication, IUser user)
        {
            medication.CreateUserId = user.Id;
            medication.CreateUser = user.Name;
            medication.CreateDate = DateTime.UtcNow;

            await _context.Medications.AddAsync(medication);
            await _context.SaveChangesAsync();
        }

        public async Task<Medications> GetMedicationByIdAsync(Guid id, Guid userId)
        {
            return await _context.Medications.FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);
        }

        public async Task<List<Medications>> GetMedicationsByUserIdAsync(Guid userId)
        {
           return await _context.Medications.Where(m => m.UserId == userId).ToListAsync();
        }

        public async Task UpdateMedicationAsync(Medications medication)
        {
            _context.Medications.Update(medication);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMedicationAsync(Guid id, Guid userId)
        {
            var medication = await _context.Medications.FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);
            if (medication != null)
            {
                _context.Medications.Remove(medication);
                await _context.SaveChangesAsync();
            }
        }
    }
}
