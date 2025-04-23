using HealthReminder.AppService.Interfaces.MedicalAppointment;
using HealthReminder.AppService.MedicalApointment;
using HealthReminder.AppService.MedicalApointment.DTOs;
using HealthReminder.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthReminder.Api.Controllers.MedicalAppointment
{
    [ApiController]
    [Authorize]
    [Route("api/{userId}/appointments")]
    public class MedicalAppointmentController : ControllerBase
    {
        private readonly IMedicalAppointmentAppService _medicalAppointmentAppService;
        private readonly IUser _user;

        public MedicalAppointmentController(IMedicalAppointmentAppService medicalAppointmentAppService, IUser user)
        {
            _medicalAppointmentAppService = medicalAppointmentAppService;
            _user = user;
        }

        [HttpPost]
        public async Task<IActionResult> AddMedicalAppointmentAsync([FromRoute] Guid userId, [FromBody] CreateMedicalAppointmentDto createMedicalAppointmentDto)
        {
            await _medicalAppointmentAppService.AddMedicalAppointmentAsync(userId, createMedicalAppointmentDto, _user);
            return Ok("Consulta criada com sucesso.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllByUser([FromRoute] Guid userId)
        {
            var medicalAppointments = await _medicalAppointmentAppService.GetMedicalAppointmentsByUserIdAsync(userId, _user);
            return Ok(medicalAppointments);
        }

        [HttpGet("{appointmentId}")]
        public async Task<IActionResult> GetMedicalAppointmentByIdAsync([FromRoute] Guid userId, [FromRoute] Guid appointmentId)
        {
            var medicalAppointment = await _medicalAppointmentAppService.GetMedicalAppointmentByIdAsync(userId, appointmentId, _user);
            return Ok(medicalAppointment);
        }

        [HttpPut("{appointmentId}")]
        public async Task<IActionResult> UpdateMedicalAppointmentAsync([FromRoute] Guid userId,
            [FromRoute] Guid appointmentId,
            [FromBody] UpdateMedicalAppointmentDto updateMedicalAppointmentDto)
        {
            await _medicalAppointmentAppService.UpdateMedicalAppointmentAsync(userId, appointmentId, updateMedicalAppointmentDto, _user);
            return Ok("Consulta atualizada com sucesso.");
        }

        [HttpDelete("{appointmentId}")]
        public async Task<IActionResult> DeleteMedicalAppointmentAsync([FromRoute] Guid userId, [FromRoute] Guid appointmentId)
        {
            await _medicalAppointmentAppService.DeleteMedicalAppointmentAsync(userId, appointmentId, _user);
            return Ok("Consulta deletada com sucesso.");
        }
    }
}
