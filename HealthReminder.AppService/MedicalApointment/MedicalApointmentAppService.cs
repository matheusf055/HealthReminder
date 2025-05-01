using HealthReminder.AppService.Exam.DTOs;
using HealthReminder.AppService.Interfaces.MedicalAppointment;
using HealthReminder.AppService.MedicalApointment.Commands;
using HealthReminder.AppService.MedicalApointment.DTOs;
using HealthReminder.Domain.Common;
using HealthReminder.Domain.MedicalAppointment;
using HealthReminder.Domain.MedicalAppointment.Repositories;

namespace HealthReminder.AppService.MedicalApointment
{
    public class MedicalApointmentAppService : IMedicalAppointmentAppService
    {
        private readonly IMedicalAppointmentRepository _medicalAppointmentRepository;

        public MedicalApointmentAppService(IMedicalAppointmentRepository medicalAppointmentRepository)
        {
            _medicalAppointmentRepository = medicalAppointmentRepository;
        }

        public async Task<CreateMedicalAppointmentDto> AddMedicalAppointmentAsync(CreateMedicalAppointmentCommand command, IUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (command == null) throw new ArgumentNullException(nameof(command));

            var medicalApoint = new MedicalAppointments(command.DoctorName, command.Specialty, command.AppointmentDateTime, command.Location, command.UserId);

            await _medicalAppointmentRepository.AddMedicalAppointmentAsync(medicalApoint, user);

            var dto = new CreateMedicalAppointmentDto {
                Id = medicalApoint.Id,
                UserId = medicalApoint.UserId,
                DoctorName = medicalApoint.DoctorName,
                Specialty = medicalApoint.Specialty,
                AppointmentDateTime = medicalApoint.AppointmentDateTime,
                Location = medicalApoint.Location,
            };

            return dto;
        }

        public async Task<MedicalAppointmentDto> GetMedicalAppointmentByIdAsync(Guid userId, Guid appointmentId, IUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var medicalAppointment = await _medicalAppointmentRepository.GetMedicalAppointmentByIdAsync(appointmentId, userId);
            if (medicalAppointment == null) throw new KeyNotFoundException("Consulta médica não encontrada.");

            return new MedicalAppointmentDto
            {
                Id = medicalAppointment.Id,
                DoctorName = medicalAppointment.DoctorName,
                Specialty = medicalAppointment.Specialty,
                AppointmentDateTime = medicalAppointment.AppointmentDateTime,
                Location = medicalAppointment.Location,
                UserId = medicalAppointment.UserId,
                Exams = medicalAppointment.Exams.Select(exam => new ExamDto
                {
                    Id = exam.Id,
                    Name = exam.Name,
                    ScheduledDate = exam.ScheduledDate,
                    SeekExamDate = exam.SeekExamDate,
                    UserId = exam.UserId,
                    MedicalAppointmentId = exam.MedicalAppointmentId
                }).ToList()
            };
        }

        public async Task<List<MedicalAppointmentDto>> GetMedicalAppointmentsByUserIdAsync(Guid userId, IUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var medicalAppointment = await _medicalAppointmentRepository.GetMedicalAppointmentsByUserIdAsync(userId);

            return medicalAppointment.Select(x => new MedicalAppointmentDto
            {
                Id = x.Id,
                DoctorName = x.DoctorName,
                Specialty = x.Specialty,
                AppointmentDateTime = x.AppointmentDateTime,
                Location = x.Location,
                UserId = x.UserId,
                Exams = x.Exams.Select(exam => new ExamDto
                {
                    Id = exam.Id,
                    Name = exam.Name,
                    ScheduledDate = exam.ScheduledDate,
                    UserId = exam.UserId,
                    MedicalAppointmentId = exam.MedicalAppointmentId,
                    SeekExamDate = exam.SeekExamDate
                }).ToList()
            }).ToList();
        }

        public async Task UpdateMedicalAppointmentAsync(Guid userId, Guid appointmentId, UpdateMedicalAppointmentDto updateMedicalAppointmentDto, IUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var medicalAppointment = await _medicalAppointmentRepository.GetMedicalAppointmentByIdAsync(appointmentId, userId);
            if (medicalAppointment == null) throw new KeyNotFoundException("Consulta médica não encontrada.");

            medicalAppointment.DoctorName = updateMedicalAppointmentDto.DoctorName;
            medicalAppointment.Specialty = updateMedicalAppointmentDto.Specialty;
            medicalAppointment.AppointmentDateTime = updateMedicalAppointmentDto.AppointmentDateTime;
            medicalAppointment.Location = updateMedicalAppointmentDto.Location;

            await _medicalAppointmentRepository.UpdateMedicalAppointmentAsync(medicalAppointment);
        }

        public async Task DeleteMedicalAppointmentAsync(Guid userId, Guid appointmentId, IUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var medicalAppointment = await _medicalAppointmentRepository.GetMedicalAppointmentByIdAsync(appointmentId, userId);
            if (medicalAppointment == null) throw new KeyNotFoundException("Consulta médica não encontrada.");

            await _medicalAppointmentRepository.DeleteMedicalAppointmentAsync(appointmentId, userId);
        }
    }
}