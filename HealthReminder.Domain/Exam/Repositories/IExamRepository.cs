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
        Task Create(Exams exam, IUser user);
        Task<Exams> GetById(Guid id, Guid userId);
        Task<List<Exams>> GetAll(Guid userId);
        Task Update(Exams exam);
        Task Delete(Guid id, Guid userId);
    }
}
