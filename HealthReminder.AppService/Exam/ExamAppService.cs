using HealthReminder.AppService.Exam.DTOs;
using HealthReminder.AppService.Interfaces.Exam;
using HealthReminder.Domain.Common;
using HealthReminder.Domain.Exams.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task AddExamAsync(Guid userId, CreateExamDto createExamDto, IUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (createExamDto == null) throw new ArgumentNullException(nameof(createExamDto));

            var exam = new Domain.Exams.Exam
                (
                    createExamDto.Name,
                    createExamDto.ScheduledDate,
                    createExamDto.SeekExamDate,
                    userId,
                    createExamDto.MedicalAppointmentId,
                    user.Id,
                    user.Name
                );

            await _examRepository.AddExamAsync(exam);
        }

        public async Task<ExamDto> GetExamByIdAsync(Guid userId, Guid examId, IUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var exam = await _examRepository.GetExamByIdAsync(examId, userId);
            if (exam == null) throw new KeyNotFoundException("Exame não encontrado.");

            return new ExamDto
            {
                Id = exam.Id,
                Name = exam.Name,
                ScheduledDate = exam.ScheduledDate,
                SeekExamDate = exam.SeekExamDate,
                UserId = exam.UserId,
                MedicalAppointmentId = exam.MedicalAppointmentId 
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
                SeekExamDate = exam.SeekExamDate,
                UserId = exam.UserId,
                MedicalAppointmentId = exam.MedicalAppointmentId 
            }).ToList();
        }

        public async Task UpdateExamAsync(Guid userId, Guid examId, UpdateExamDto updateExamDto, IUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var exam = await _examRepository.GetExamByIdAsync(examId, userId);
            if (exam == null) throw new KeyNotFoundException("Exame não encontrado.");

            exam.Name = updateExamDto.Name;
            exam.ScheduledDate = updateExamDto.ScheduledDate;
            exam.SeekExamDate = updateExamDto.SeekExamDate;
            exam.MedicalAppointmentId = updateExamDto.MedicalAppointmentId; 

            await _examRepository.UpdateExamAsync(exam);
        }

        public async Task DeleteExamByIdAsync(Guid userId, Guid examId, IUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var exam = await _examRepository.GetExamByIdAsync(examId, userId);
            if (exam == null) throw new KeyNotFoundException("Exame não encontrado.");

            await _examRepository.DeleteExamByIdAsync(examId, userId);
        }
    }
}
