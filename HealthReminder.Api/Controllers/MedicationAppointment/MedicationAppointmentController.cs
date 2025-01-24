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
            await _medicationAppointmentAppService.AddMedicalAppointmentAsync(createMedicalAppointmentDto, _user);
            return Ok("Consulta criada com sucesso.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllByUser()
        {
            var medicalAppointments = await _medicationAppointmentAppService.GetMedicalAppointmentsByUserIdAsync(_user.Id, _user);
            return Ok(medicalAppointments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMedicalAppointmentByIdAsync(Guid id)
        {
            var medicalAppointment = await _medicationAppointmentAppService.GetMedicalAppointmentByIdAsync(id, _user);
            return Ok(medicalAppointment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMedicalAppointmentAsync(Guid id, [FromBody] UpdateMedicalAppointmentDto updateMedicalAppointmentDto)
        {
            await _medicationAppointmentAppService.UpdateMedicalAppointmentAsync(id, updateMedicalAppointmentDto, _user);
            return Ok("Consulta atualizada com sucesso.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicalAppointmentAsync(Guid id)
        {
            await _medicationAppointmentAppService.DeleteMedicalAppointmentAsync(id, _user);
            return Ok("Consulta deletada com sucesso.");
        }
    }
}
