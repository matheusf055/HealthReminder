using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthReminder.AppService.Exam.Commands;
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

        public async Task<MedicationDto> Create(CreateMedicationCommand command, IUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (command == null) throw new ArgumentNullException(nameof(command));

            var medication = new Medications(command.Name, command.Dosage, command.Frequency, command.TotalPills, command.UserId);

            await _medicationRepository.Create(medication, user);

            return new MedicationDto
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
        }

        public async Task Take(Guid userId, Guid medicationId, IUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var medication = await _medicationRepository.GetById(medicationId, userId);
            if (medication == null) throw new KeyNotFoundException("Medicação não encontrada.");

            medication.TakePill();
            await _medicationRepository.Update(medication);
        }

        public async Task<MedicationDto> GetById(Guid userId, Guid medicationId, IUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var medication = await _medicationRepository.GetById(medicationId, userId);
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

        public async Task<List<MedicationDto>> GetAll(Guid userId, IUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var medications = await _medicationRepository.GetAll(userId);

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

        public async Task Update(UpdateMedicationCommand command, IUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (command == null) throw new ArgumentNullException(nameof(command));

            var medication = await _medicationRepository.GetById(command.Id, command.UserId);
            if (medication == null) throw new KeyNotFoundException("Medicação não encontrada");

            medication.Name = command.Name;
            medication.Dosage = command.Dosage;
            medication.Frequency = command.Frequency;
            medication.TotalPills = command.TotalPills;

            await _medicationRepository.Update(medication);
        }

        public async Task Delete(Guid userId, Guid medicationId, IUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var medication = await _medicationRepository.GetById(medicationId, userId);
            if (medication == null) throw new KeyNotFoundException("Medicação não encontrada");

            await _medicationRepository.Delete(medicationId, userId);
        }
    }
}