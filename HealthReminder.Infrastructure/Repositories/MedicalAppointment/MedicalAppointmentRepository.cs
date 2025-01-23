using HealthReminder.Domain.MedicalAppointments.Repositories;
using HealthReminder.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthReminder.Infrastructure.Repositories.MedicalAppointment
{
    public class MedicalAppointmentRepository : IMedicalAppointmentRepository
    {
        private readonly HealthReminderDbContext _context;

        public MedicalAppointmentRepository(HealthReminderDbContext context)
        {
            _context = context;
        }

        public async Task AddMedicalAppointmentAsync(Domain.MedicalAppointments.MedicalAppointment medicalAppointment)
        {
            await _context.MedicalAppointments.AddAsync(medicalAppointment);
            await _context.SaveChangesAsync();
        }

        public async Task<Domain.MedicalAppointments.MedicalAppointment> GetMedicalAppointmentByIdAsync(Guid id, Guid userId)
        {
           return await _context.MedicalAppointments.FirstOrDefaultAsync(ma => ma.Id == id && ma.UserId == userId);    
        }

        public async Task<List<Domain.MedicalAppointments.MedicalAppointment>> GetMedicalAppointmentsByUserIdAsync(Guid userId)
        {
            return await _context.MedicalAppointments.Where(ma => ma.UserId == userId).ToListAsync();
        }

        public async Task UpdateMedicalAppointmentAsync(Domain.MedicalAppointments.MedicalAppointment medicalAppointment)
        {
           _context.MedicalAppointments.Update(medicalAppointment);
           await _context.SaveChangesAsync();
        }

        public async Task DeleteMedicalAppointmentAsync(Guid id, Guid userId)
        {
            var medicalAppointment = await _context.MedicalAppointments.FirstOrDefaultAsync(ma => ma.Id == id && ma.UserId == userId);
            if (medicalAppointment != null)
            {
                _context.MedicalAppointments.Remove(medicalAppointment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
