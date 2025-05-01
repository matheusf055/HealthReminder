using HealthReminder.AppService.Exam.Commands;
using HealthReminder.AppService.Exam.DTOs;
using HealthReminder.AppService.Interfaces.Exam;
using HealthReminder.Domain.Common;
using HealthReminder.Domain.Exam;
using HealthReminder.Domain.Exam.Repositories;
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

        public async Task<ExamDto> Create(CreateExamCommand command, IUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (command == null) throw new ArgumentNullException(nameof(command));

            var exam = new Exams(command.Name, command.ScheduledDate, command.SeekExamDate, command.UserId, command.MedicalAppointmentId);

            await _examRepository.Create(exam, user);

            var dto = new ExamDto
            {
                Id = exam.Id,
                Name = exam.Name,
                ScheduledDate = exam.ScheduledDate,
                SeekExamDate = exam.SeekExamDate,
                UserId = exam.UserId,
                MedicalAppointmentId = exam.MedicalAppointmentId
            };

            return dto;
        }

        public async Task<ExamDto> GetById(Guid userId, Guid examId, IUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var exam = await _examRepository.GetById(examId, userId);
            if (exam == null) throw new KeyNotFoundException("Exame não encontrado.");

            var dto = new ExamDto
            {
                Id = exam.Id,
                Name = exam.Name,
                ScheduledDate = exam.ScheduledDate,
                SeekExamDate = exam.SeekExamDate,
                UserId = exam.UserId,
                MedicalAppointmentId = exam.MedicalAppointmentId
            };

            return dto;
        }

        public async Task<List<ExamDto>> GetAll(Guid userId, IUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var exams = await _examRepository.GetAll(userId);
            var dto = exams.Select(exam => new ExamDto
            {
                Id = exam.Id,
                Name = exam.Name,
                ScheduledDate = exam.ScheduledDate,
                SeekExamDate = exam.SeekExamDate,
                UserId = exam.UserId,
                MedicalAppointmentId = exam.MedicalAppointmentId
            }).ToList();

            return dto;
        }

        public async Task Update(UpdateExamCommand command, IUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var exam = await _examRepository.GetById(command.Id, command.UserId);
            if (exam == null) throw new KeyNotFoundException("Exame não encontrado.");

            exam.Name = command.Name;
            exam.ScheduledDate = command.ScheduledDate;
            exam.SeekExamDate = command.SeekExamDate;
            exam.MedicalAppointmentId = command.MedicalAppointmentId; 

            await _examRepository.Update(exam);
        }

        public async Task Delete(Guid userId, Guid examId, IUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var exam = await _examRepository.GetById(examId, userId);
            if (exam == null) throw new KeyNotFoundException("Exame não encontrado.");

            await _examRepository.Delete(examId, userId);
        }
    }
}
