using HealthReminder.Domain.Exams;
using HealthReminder.Domain.Exams.Repositories;
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

        public async Task AddExamAsync(Exams exam)
        {
            await _context.Exams.AddAsync(exam);
            await _context.SaveChangesAsync();
        }

        public async Task<Exams> GetExamByIdAsync(Guid id, Guid userId)
        {
            return await _context.Exams.FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);
        }

        public async Task<List<Exams>> GetExamsByUserIdAsync(Guid userId)
        {
            return await _context.Exams.Where(e => e.UserId == userId).ToListAsync();
        }

        public async Task UpdateExamAsync(Exams exam)
        {
            _context.Exams.Update(exam);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteExamByIdAsync(Guid id, Guid userId)
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
