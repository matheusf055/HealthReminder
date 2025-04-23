using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthReminder.Domain.Exams.Repositories
{
    public interface IExamRepository
    {
        Task AddExamAsync(Exams exam);
        Task<Exams> GetExamByIdAsync(Guid id, Guid userId);
        Task<List<Exams>> GetExamsByUserIdAsync(Guid userId);
        Task UpdateExamAsync(Exams exam);
        Task DeleteExamByIdAsync(Guid id, Guid userId);
    }
}
