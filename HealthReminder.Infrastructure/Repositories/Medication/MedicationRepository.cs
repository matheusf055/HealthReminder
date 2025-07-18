﻿using HealthReminder.Domain.Common;
using HealthReminder.Domain.Exam;
using HealthReminder.Domain.Medication;
using HealthReminder.Domain.Medication.Repositories;
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

        public async Task Create(Medications medication, IUser user)
        {
            medication.CreateUserId = user.Id;
            medication.CreateUser = user.Name;
            medication.CreateDate = DateTime.UtcNow;

            await _context.Medications.AddAsync(medication);
           await _context.SaveChangesAsync();
        }

        public async Task<Medications> GetById(Guid id, Guid userId)
        {
            return await _context.Medications.FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);
        }

        public async Task<List<Medications>> GetAll(Guid userId)
        {
           return await _context.Medications.Where(m => m.UserId == userId).ToListAsync();
        }

        public async Task Update(Medications medication)
        {
            _context.Medications.Update(medication);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id, Guid userId)
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
