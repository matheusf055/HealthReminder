using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthReminder.AppService.Interfaces.Medication;
using HealthReminder.AppService.Medication.DTOs;
using HealthReminder.Domain.Common;
using HealthReminder.Domain.Medications.Repositories;

namespace HealthReminder.AppService.Medication
{
    public class MedicationAppService : IMedicationAppService
    {
        private readonly IMedicationRepository _medicationRepository;

        public MedicationAppService(IMedicationRepository medicationRepository)
        {
            _medicationRepository = medicationRepository;
        }

        public async Task AddMedicationAsync(CreateMedicationDto createMedicationDto, IUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (createMedicationDto == null) throw new ArgumentNullException(nameof(createMedicationDto));

            var medication = new Domain.Medications.Medication(
                createMedicationDto.Name,
                createMedicationDto.Dosage,
                createMedicationDto.Frequency,
                createMedicationDto.TotalPills,
                user.Id,
                user.Id,
                user.Name
            );

            await _medicationRepository.AddMedicationAsync(medication);
        }

        public async Task TakeMedicationAsync(Guid id, IUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var medication = await _medicationRepository.GetMedicationByIdAsync(id, user.Id);
            if (medication == null) throw new KeyNotFoundException("Medicação não encontrada");

            medication.TakePill();
            await _medicationRepository.UpdateMedicationAsync(medication);
        }

        public async Task<MedicationDto> GetMedicationByIdAsync(Guid id, IUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var medication = await _medicationRepository.GetMedicationByIdAsync(id, user.Id);
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
                UserId = user.Id
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
                UserId = userId
            }).ToList();
        }

        public async Task UpdateMedicationAsync(Guid id, UpdateMedicationDto updateMedicationDto, IUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (updateMedicationDto == null) throw new ArgumentNullException(nameof(updateMedicationDto));

            var medication = await _medicationRepository.GetMedicationByIdAsync(id, user.Id);
            if (medication == null) throw new KeyNotFoundException("Medicação não encontrada");

            medication.Name = updateMedicationDto.Name;
            medication.Dosage = updateMedicationDto.Dosage;
            medication.Frequency = updateMedicationDto.Frequency;
            medication.TotalPills = updateMedicationDto.TotalPills;
            medication.AlertThreshold = updateMedicationDto.AlertThreshold;
            medication.IsLowStockAlertSent = updateMedicationDto.IsLowStockAlertSent;

            await _medicationRepository.UpdateMedicationAsync(medication);
        }

        public async Task DeleteMedicationAsync(Guid id, IUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var medication = await _medicationRepository.GetMedicationByIdAsync(id, user.Id);
            if (medication == null) throw new KeyNotFoundException("Medicação não encontrada");

            await _medicationRepository.DeleteMedicationAsync(id, user.Id);
        }
    }
}