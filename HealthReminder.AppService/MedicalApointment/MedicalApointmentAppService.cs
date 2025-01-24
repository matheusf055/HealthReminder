using HealthReminder.AppService.Interfaces.MedicalAppointment;
using HealthReminder.AppService.MedicalApointment.DTOs;
using HealthReminder.Domain.Common;
using HealthReminder.Domain.MedicalAppointments;
using HealthReminder.Domain.MedicalAppointments.Repositories;

namespace HealthReminder.AppService.MedicalApointment
{
    public class MedicalApointmentAppService : IMedicalAppointmentAppService
    {
        private readonly IMedicalAppointmentRepository _medicalAppointmentRepository;

        public MedicalApointmentAppService(IMedicalAppointmentRepository medicalAppointmentRepository)
        {
            _medicalAppointmentRepository = medicalAppointmentRepository;
        }

        public async Task AddMedicalAppointmentAsync(CreateMedicalAppointmentDto createMedicalAppointmentDto, IUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (createMedicalAppointmentDto == null) throw new ArgumentNullException(nameof(createMedicalAppointmentDto));

            var specialty = createMedicalAppointmentDto.Specialty ?? string.Empty;

            var medicalApoint = new MedicalAppointment
            (
                createMedicalAppointmentDto.DoctorName,
                specialty,
                createMedicalAppointmentDto.AppointmentDateTime,
                createMedicalAppointmentDto.Location,
                user.Id,
                user.Id,
                user.Name 
            );

            await _medicalAppointmentRepository.AddMedicalAppointmentAsync(medicalApoint);
        }

        public async Task<MedicalAppointmentDto> GetMedicalAppointmentByIdAsync(Guid id, IUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var medicalAppointment = await _medicalAppointmentRepository.GetMedicalAppointmentByIdAsync(id, user.Id);

            return new MedicalAppointmentDto
            {
                Id = medicalAppointment.Id,
                DoctorName = medicalAppointment.DoctorName,
                Specialty = medicalAppointment.Specialty,
                AppointmentDateTime = medicalAppointment.AppointmentDateTime,
                Location = medicalAppointment.Location,
                UserId = medicalAppointment.UserId,
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
            }).ToList();
        }

        public async Task UpdateMedicalAppointmentAsync(Guid id, UpdateMedicalAppointmentDto updateMedicalAppointmentDto, IUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var medicalAppointment = await _medicalAppointmentRepository.GetMedicalAppointmentByIdAsync(id, user.Id);
            if (medicalAppointment == null) return;

            medicalAppointment.DoctorName = updateMedicalAppointmentDto.DoctorName;
            medicalAppointment.Specialty = updateMedicalAppointmentDto.Specialty;
            medicalAppointment.AppointmentDateTime = updateMedicalAppointmentDto.AppointmentDateTime;
            medicalAppointment.Location = updateMedicalAppointmentDto.Location;

            await _medicalAppointmentRepository.UpdateMedicalAppointmentAsync(medicalAppointment);
        }

        public async Task DeleteMedicalAppointmentAsync(Guid id, IUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var medicalAppointment = await _medicalAppointmentRepository.GetMedicalAppointmentByIdAsync(id, user.Id);
            if (medicalAppointment == null) return;

            await _medicalAppointmentRepository.DeleteMedicalAppointmentAsync(id, user.Id);
        }
    }
}
