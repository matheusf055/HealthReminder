using HealthReminder.Domain.Common;
using HealthReminder.Domain.MedicalAppointment;
using HealthReminder.Domain.MedicalAppointment.Repositories;
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

        public async Task Create(MedicalAppointments medicalAppointment, IUser user)
        {
            medicalAppointment.CreateUserId = user.Id;
            medicalAppointment.CreateUser = user.Name;
            medicalAppointment.CreateDate = DateTime.UtcNow;

            await _context.MedicalAppointments.AddAsync(medicalAppointment);
            await _context.SaveChangesAsync();
        }

        public async Task<MedicalAppointments> GetById(Guid id, Guid userId)
        {
           return await _context.MedicalAppointments
                .Include(ma => ma.Exams)
                .FirstOrDefaultAsync(ma => ma.Id == id && ma.UserId == userId);    
        }

        public async Task<List<MedicalAppointments>> GetAll(Guid userId)
        {
            return await _context.MedicalAppointments
                .Include(ma => ma.Exams)
                .Where(ma => ma.UserId == userId)
                .ToListAsync();
        }

        public async Task Update(MedicalAppointments medicalAppointment)
        {
           _context.MedicalAppointments.Update(medicalAppointment);
           await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id, Guid userId)
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
