using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthReminder.Domain.Common;

namespace HealthReminder.Domain.Exam.Repositories
{
    public interface IExamRepository
    {
        Task AddExamAsync(Exams exam, IUser user);
        Task<Exams> GetExamByIdAsync(Guid id, Guid userId);
        Task<List<Exams>> GetExamsByUserIdAsync(Guid userId);
        Task UpdateExamAsync(Exams exam);
        Task DeleteExamByIdAsync(Guid id, Guid userId);
    }
}
