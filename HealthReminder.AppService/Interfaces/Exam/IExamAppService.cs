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
        Task<CreateExamDto> Create(CreateExamCommand command, IUser user);
        Task<ExamDto> GetById(Guid userId, Guid examId, IUser user);
        Task<List<ExamDto>> GetAll(Guid userId, IUser user);
        Task Update(Guid userId, Guid examId, UpdateExamDto updateExamDto, IUser user);
        Task Delete(Guid userId, Guid examId, IUser user);
    }
}
