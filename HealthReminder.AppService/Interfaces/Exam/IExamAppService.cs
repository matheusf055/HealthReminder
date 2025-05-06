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
        Task<ExamDto> Create(CreateExamCommand command, IUser user);
        Task<ExamDto> GetById(Guid examId, IUser user);
        Task<List<ExamDto>> GetAll(IUser user);
        Task Update(UpdateExamCommand command, IUser user);
        Task Delete(Guid examId, IUser user);
    }
}
