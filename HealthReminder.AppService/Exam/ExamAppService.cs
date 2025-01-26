using HealthReminder.AppService.Exam.DTOs;
using HealthReminder.AppService.Interfaces.Exam;
using HealthReminder.AppService.MedicalApointment.DTOs;
using HealthReminder.Domain.Common;
using HealthReminder.Domain.Exams.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthReminder.AppService.Exam
{
    public class ExamAppService : IExamAppService
    {
        private readonly IExamRepository _examRepository;

        public ExamAppService(IExamRepository examRepository)
        {
            _examRepository = examRepository;
        }

        public async Task AddExamAsync(CreateExamDto createExamDto, IUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if(createExamDto == null) throw new ArgumentNullException(nameof(createExamDto));

            var exam = new Domain.Exams.Exam
                (createExamDto.Name, 
                createExamDto.ScheduledDate, 
                createExamDto.SeekExam, 
                user.Id,
                user.Id,
                user.Name);

            await _examRepository.AddExamAsync(exam);
        }

        
        public async Task<ExamDto> GetExamByIdAsync(Guid id, IUser user)
        {
           if(user == null) throw new ArgumentNullException(nameof(user));

           var exam = await _examRepository.GetExamByIdAsync(id, user.Id);
           if (exam == null) throw new KeyNotFoundException("Exame não encontrado.");

           return new ExamDto
           {
               Id = exam.Id,
               Name = exam.Name,
               ScheduledDate = exam.ScheduledDate,
               SeekExam = exam.SeekExam,
               UserId = exam.UserId
           };
        }

        public async Task<List<ExamDto>> GetExamsByUserIdAsync(Guid userId, IUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var exams = await _examRepository.GetExamsByUserIdAsync(userId);

            return exams.Select(exam => new ExamDto
            {
                Id = exam.Id,
                Name = exam.Name,
                ScheduledDate = exam.ScheduledDate,
                SeekExam = exam.SeekExam,
                UserId = exam.UserId
            }).ToList();
        }

        public async Task UpdateExamAsync(Guid id, UpdateExamDto updateExamDto, IUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var exams = await _examRepository.GetExamByIdAsync(id, user.Id);
            if (exams == null) throw new KeyNotFoundException("Exame não encontrado.");

            exams.Name = updateExamDto.Name;
            exams.ScheduledDate = updateExamDto.ScheduledDate;
            exams.SeekExam = updateExamDto.SeekExam;

            await _examRepository.UpdateExamAsync(exams);
        }

        public async Task DeleteExamByIdAsync(Guid id, Guid userId, IUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var exams = await _examRepository.GetExamByIdAsync(id, user.Id);
            if (exams == null) throw new KeyNotFoundException("Exame não encontrado.");

            await _examRepository.DeleteExamByIdAsync(id, user.Id);
        }
    }
}
