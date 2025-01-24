using HealthReminder.AppService.Interfaces.MedicalAppointment;
using HealthReminder.AppService.MedicalApointment.DTOs;
using HealthReminder.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthReminder.Api.Controllers.MedicationAppointment
{
    [ApiController]
    [Authorize]
    [Route("api/medicationAppointment")]
    public class MedicationAppointmentController : ControllerBase
    {
        private readonly IMedicalAppointmentAppService _medicationAppointmentAppService;
        private readonly IUser _user;

        public MedicationAppointmentController(IMedicalAppointmentAppService medicationAppointmentAppService, IUser user)
        {
            _medicationAppointmentAppService = medicationAppointmentAppService;
            _user = user;
        }

        [HttpPost]
        public async Task<IActionResult> AddMedicalAppointmentAsync(CreateMedicalAppointmentDto createMedicalAppointmentDto)
        {
            try
            {
                await _medicationAppointmentAppService.AddMedicalAppointmentAsync(createMedicalAppointmentDto, _user);
                return Ok("Consulta criada com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllByUser()
        {
            try
            {
                var medicalAppointments = await _medicationAppointmentAppService.GetMedicalAppointmentsByUserIdAsync(_user.Id, _user);
                return Ok(medicalAppointments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMedicalAppointmentByIdAsync(Guid id)
        {
            try
            {
                var medicalAppointment = await _medicationAppointmentAppService.GetMedicalAppointmentByIdAsync(id, _user);
                return Ok(medicalAppointment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMedicalAppointmentAsync(Guid id, [FromBody] UpdateMedicalAppointmentDto updateMedicalAppointmentDto)
        {
            try
            {
                await _medicationAppointmentAppService.UpdateMedicalAppointmentAsync(id, updateMedicalAppointmentDto, _user);
                return Ok("Consulta atualizada com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicalAppointmentAsync(Guid id)
        {
            try
            {
                await _medicationAppointmentAppService.DeleteMedicalAppointmentAsync(id, _user);
                return Ok("Consulta deletada com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
