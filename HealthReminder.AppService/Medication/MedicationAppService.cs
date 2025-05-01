using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthReminder.AppService.Interfaces.Medication;
using HealthReminder.AppService.Medication.Commands;
using HealthReminder.AppService.Medication.DTOs;
using HealthReminder.Domain.Common;
using HealthReminder.Domain.Medication;
using HealthReminder.Domain.Medication.Repositories;

namespace HealthReminder.AppService.Medication
{
    public class MedicationAppService : IMedicationAppService
    {
        private readonly IMedicationRepository _medicationRepository;

        public MedicationAppService(IMedicationRepository medicationRepository)
        {
            _medicationRepository = medicationRepository;
        }

        public async Task<MedicationDto> AddMedicationAsync(CreateMedicationCommand command, IUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (command == null) throw new ArgumentNullException(nameof(command));

            var medication = new Medications(command.Name, command.Dosage, command.Frequency, command.TotalPills, command.UserId);

            await _medicationRepository.AddMedicationAsync(medication, user);

            var dto = new MedicationDto
            {
                Id = medication.Id,
                Name = medication.Name,
                Dosage = medication.Dosage,
                Frequency = medication.Frequency,
                TotalPills = medication.TotalPills,
                AlertThreshold = medication.AlertThreshold,
                IsLowStockAlertSent = medication.IsLowStockAlertSent,
                UserId = command.UserId
            };

            return dto;
        }

        public async Task TakeMedicationAsync(Guid userId, Guid medicationId, IUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var medication = await _medicationRepository.GetMedicationByIdAsync(medicationId, userId);
            if (medication == null) throw new KeyNotFoundException("Medicação não encontrada.");

            medication.TakePill();
            await _medicationRepository.UpdateMedicationAsync(medication);
        }

        public async Task<MedicationDto> GetMedicationByIdAsync(Guid userId, Guid medicationId, IUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var medication = await _medicationRepository.GetMedicationByIdAsync(medicationId, userId);
            if (medication == null) throw new KeyNotFoundException("Medicação não encontrada");

            return new MedicationDto
            {
                Id = medication.Id,
                Name = medication.Name,
                Dosage = medication.Dosage,
                Frequency = medication.Frequency,
                TotalPills = medication.TotalPills,
                AlertThreshold = medication.AlertThreshold,
                IsLowStockAlertSent = medication.IsLowStockAlertSent,
                UserId = medication.UserId
            };
        }

        public async Task<List<MedicationDto>> GetMedicationsByUserIdAsync(Guid userId, IUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var medications = await _medicationRepository.GetMedicationsByUserIdAsync(userId);

            return medications.Select(medication => new MedicationDto
            {
                Id = medication.Id,
                Name = medication.Name,
                Dosage = medication.Dosage,
                Frequency = medication.Frequency,
                TotalPills = medication.TotalPills,
                AlertThreshold = medication.AlertThreshold,
                IsLowStockAlertSent = medication.IsLowStockAlertSent,
                UserId = medication.UserId
            }).ToList();
        }

        public async Task UpdateMedicationAsync(Guid userId, Guid medicationId, UpdateMedicationDto updateMedicationDto, IUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (updateMedicationDto == null) throw new ArgumentNullException(nameof(updateMedicationDto));

            var medication = await _medicationRepository.GetMedicationByIdAsync(medicationId, userId);
            if (medication == null) throw new KeyNotFoundException("Medicação não encontrada");

            medication.Name = updateMedicationDto.Name;
            medication.Dosage = updateMedicationDto.Dosage;
            medication.Frequency = updateMedicationDto.Frequency;
            medication.TotalPills = updateMedicationDto.TotalPills;
            medication.AlertThreshold = updateMedicationDto.AlertThreshold;
            medication.IsLowStockAlertSent = updateMedicationDto.IsLowStockAlertSent;

            await _medicationRepository.UpdateMedicationAsync(medication);
        }

        public async Task DeleteMedicationAsync(Guid userId, Guid medicationId, IUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var medication = await _medicationRepository.GetMedicationByIdAsync(medicationId, userId);
            if (medication == null) throw new KeyNotFoundException("Medicação não encontrada");

            await _medicationRepository.DeleteMedicationAsync(medicationId, userId);
        }
    }
}