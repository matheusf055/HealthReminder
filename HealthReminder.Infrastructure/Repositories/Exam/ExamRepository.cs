using HealthReminder.Domain.Common;
using HealthReminder.Domain.Exam;
using HealthReminder.Domain.Exam.Repositories;
using HealthReminder.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HealthReminder.Infrastructure.Repositories.Exam
{
    public class ExamRepository : IExamRepository
    {
        private readonly HealthReminderDbContext _context;

        public ExamRepository(HealthReminderDbContext context)
        {
            _context = context;
        }

        public async Task Create(Exams exam, IUser user)
        {
            exam.CreateUserId = user.Id;
            exam.CreateUser = user.Name;
            exam.CreateDate = DateTime.UtcNow;

            await _context.Exams.AddAsync(exam);
            await _context.SaveChangesAsync();
        }

        public async Task<Exams> GetById(Guid id, Guid userId)
        {
            return await _context.Exams.FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);
        }

        public async Task<List<Exams>> GetAll(Guid userId)
        {
            return await _context.Exams.Where(e => e.UserId == userId).ToListAsync();
        }

        public async Task Update(Exams exam)
        {
            _context.Exams.Update(exam);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id, Guid userId)
        {
            var exam = await _context.Exams.FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);
            if (exam != null)
            {
                _context.Exams.Remove(exam);
                await _context.SaveChangesAsync();
            }
        }
    }
}
