using HealthReminder.AppService.Interfaces.Medication;
using HealthReminder.AppService.Medication.DTOs;
using HealthReminder.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HealthReminder.Api.Controllers.Medication
{
    [ApiController]
    [Authorize]
    [Route("api/{userId}/medications")]
    public class MedicationController : ControllerBase
    {
        private readonly IMedicationAppService _medicationAppService;
        private readonly IUser _user;

        public MedicationController(IMedicationAppService medicationAppService, IUser user)
        {
            _medicationAppService = medicationAppService;
            _user = user;
        }

        [HttpPost]
        public async Task<IActionResult> AddMedicationAsync([FromRoute] Guid userId, [FromBody] CreateMedicationDto createMedicationDto)
        {
            await _medicationAppService.AddMedicationAsync(userId, createMedicationDto, _user);
            return Ok("Medicação criada com sucesso.");
        }

        [HttpPost("{medicationId}/take")]
        public async Task<IActionResult> TakeMedication([FromRoute] Guid userId, [FromRoute] Guid medicationId)
        {
            await _medicationAppService.TakeMedicationAsync(userId, medicationId, _user);
            return Ok("Medicação registrada como tomada.");
        }

        [HttpGet]
        public async Task<IActionResult> GetMedicationsByUserIdAsync([FromRoute] Guid userId)
        {
            var medications = await _medicationAppService.GetMedicationsByUserIdAsync(userId, _user);
            return Ok(medications);
        }

        [HttpGet("{medicationId}")]
        public async Task<IActionResult> GetMedicationByIdAsync([FromRoute] Guid userId, [FromRoute] Guid medicationId)
        {
            var medication = await _medicationAppService.GetMedicationByIdAsync(userId, medicationId, _user);
            return Ok(medication);
        }

        [HttpPut("{medicationId}")]
        public async Task<IActionResult> UpdateMedicationAsync([FromRoute] Guid userId,
            [FromRoute] Guid medicationId,
            [FromBody] UpdateMedicationDto updateMedicationDto)
        {
            await _medicationAppService.UpdateMedicationAsync(userId, medicationId, updateMedicationDto, _user);
            return Ok("Medicação atualizada com sucesso.");
        }

        [HttpDelete("{medicationId}")]
        public async Task<IActionResult> DeleteMedicationAsync(
            [FromRoute] Guid userId,
            [FromRoute] Guid medicationId)
        {
            await _medicationAppService.DeleteMedicationAsync(userId, medicationId, _user);
            return Ok("Medicação deletada com sucesso.");
        }
    }
}
