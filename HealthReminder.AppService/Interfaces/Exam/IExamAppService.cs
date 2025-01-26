using HealthReminder.AppService.Exam.DTOs;
using HealthReminder.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthReminder.AppService.Interfaces.Exam
{
    public interface IExamAppService
    {
        Task AddExamAsync(CreateExamDto createExamDto, IUser user);
        Task<ExamDto> GetExamByIdAsync(Guid id, IUser user);
        Task<List<ExamDto>> GetExamsByUserIdAsync(Guid userId, IUser user);
        Task UpdateExamAsync(Guid id, UpdateExamDto updateExamDto, IUser user);
        Task DeleteExamByIdAsync(Guid id, Guid userId, IUser user);
    }
}
