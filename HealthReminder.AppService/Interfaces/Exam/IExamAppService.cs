using HealthReminder.AppService.Exam.Commands;
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
        Task<CreateExamDto> AddExamAsync(CreateExamCommand command, IUser user);
        Task<ExamDto> GetExamByIdAsync(Guid userId, Guid examId, IUser user);
        Task<List<ExamDto>> GetExamsByUserIdAsync(Guid userId, IUser user);
        Task UpdateExamAsync(Guid userId, Guid examId, UpdateExamDto updateExamDto, IUser user);
        Task DeleteExamByIdAsync(Guid userId, Guid examId, IUser user);
    }
}
