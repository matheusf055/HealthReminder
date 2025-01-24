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
    [Route("api/medication")]
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
        public async Task<IActionResult> AddMedicationAsync(CreateMedicationDto createMedicationDto)
        {
            try
            {
                await _medicationAppService.AddMedicationAsync(createMedicationDto, _user);
                return Ok("Medicação criada com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}/take")]
        public async Task<IActionResult> TakeMedication(Guid id)
        {
            try
            {
                await _medicationAppService.TakeMedicationAsync(id, _user);
                return Ok("Medicação registrada como tomada.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllByUser()
        {
            var medications = await _medicationAppService.GetMedicationsByUserIdAsync(_user.Id, _user);
            return Ok(medications);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMedicationByIdAsync(Guid id)
        {
            var medication = await _medicationAppService.GetMedicationByIdAsync(id, _user);
            if (medication == null)
            {
                return NotFound();
            }
            return Ok(medication);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMedicationAsync(Guid id, UpdateMedicationDto updateMedicationDto)
        {
            try
            {
                await _medicationAppService.UpdateMedicationAsync(id, updateMedicationDto, _user);
                return Ok("Medicação atualizada com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicationAsync(Guid id)
        {
            try
            {
                await _medicationAppService.DeleteMedicationAsync(id, _user);
                return Ok("Medicação deletada com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
