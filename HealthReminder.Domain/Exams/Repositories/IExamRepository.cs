using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthReminder.Domain.Exams.Repositories
{
    public interface IExamRepository
    {
        Task AddExamAsync(Exam exam);
        Task<Exam> GetExamByIdAsync(Guid id, Guid userId);
        Task<List<Exam>> GetExamsByUserIdAsync(Guid userId);
        Task UpdateExamAsync(Exam exam);
        Task DeleteExamByIdAsync(Guid id, Guid userId);
    }
}
