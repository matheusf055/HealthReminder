﻿using HealthReminder.AppService.Exam.DTOs;
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

        public async Task<MedicalAppointmentDto> Create(CreateMedicalAppointmentCommand command, IUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (command == null) throw new ArgumentNullException(nameof(command));

            var medicalApoint = new MedicalAppointments(command.DoctorName, command.Specialty, command.AppointmentDateTime, command.Location, command.UserId);

            await _medicalAppointmentRepository.Create(medicalApoint, user);

            var dto = new MedicalAppointmentDto
            {
                Id = medicalApoint.Id,
                UserId = medicalApoint.UserId,
                DoctorName = medicalApoint.DoctorName,
                Specialty = medicalApoint.Specialty,
                AppointmentDateTime = medicalApoint.AppointmentDateTime,
                Location = medicalApoint.Location,
            };

            return dto;
        }

        public async Task<MedicalAppointmentDto> GetById(Guid appointmentId, IUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var medicalAppointment = await _medicalAppointmentRepository.GetById(appointmentId, user.Id);
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
                })
            };
        }

        public async Task<List<MedicalAppointmentDto>> GetAll(IUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var medicalAppointment = await _medicalAppointmentRepository.GetAll(user.Id);

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
                })
            }).ToList();
        }

        public async Task Update(UpdateMedicalAppointmentCommand command, IUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var medicalAppointment = await _medicalAppointmentRepository.GetById(command.Id, command.UserId);
            if (medicalAppointment == null) throw new KeyNotFoundException("Consulta médica não encontrada.");

            medicalAppointment.DoctorName = command.DoctorName;
            medicalAppointment.Specialty = command.Specialty;
            medicalAppointment.AppointmentDateTime = command.AppointmentDateTime;
            medicalAppointment.Location = command.Location;

            await _medicalAppointmentRepository.Update(medicalAppointment);
        }

        public async Task Delete(Guid appointmentId, IUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var medicalAppointment = await _medicalAppointmentRepository.GetById(appointmentId, user.Id);
            if (medicalAppointment == null) throw new KeyNotFoundException("Consulta médica não encontrada.");

            await _medicalAppointmentRepository.Delete(appointmentId, user.Id);
        }
    }
}