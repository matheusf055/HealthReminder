using HealthReminder.AppService.Interfaces.MedicalAppointment;
using HealthReminder.AppService.MedicalApointment.Commands;
using HealthReminder.AppService.MedicalApointment.DTOs;
using HealthReminder.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
        [SwaggerOperation(
            Summary = "Adiciona uma nova consulta médica",
            Description = "Cria um novo registro de consulta médica para o usuário"
        )]
        [SwaggerResponse(200, "Consulta criada com sucesso")]
        [SwaggerResponse(400, "Dados inválidos fornecidos")]
        [SwaggerResponse(401, "Não autorizado")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AddMedicalAppointmentAsync([FromBody] CreateMedicalAppointmentCommand command)
        {
            var result = await _medicalAppointmentAppService.AddMedicalAppointmentAsync(command, _user);
            return Ok(result);
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Lista todas as consultas médicas do usuário",
            Description = "Retorna todas as consultas médicas cadastradas para o usuário"
        )]
        [SwaggerResponse(200, "Lista de consultas retornada com sucesso")]
        [SwaggerResponse(401, "Não autorizado")]
        [ProducesResponseType(typeof(IEnumerable<MedicalAppointmentDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllByUser([FromRoute] Guid userId)
        {
            var medicalAppointments = await _medicalAppointmentAppService.GetMedicalAppointmentsByUserIdAsync(userId, _user);
            return Ok(medicalAppointments);
        }

        [HttpGet("{appointmentId}")]
        [SwaggerOperation(
            Summary = "Obtém detalhes de uma consulta médica específica",
            Description = "Retorna os detalhes de uma consulta médica específica pelo ID"
        )]
        [SwaggerResponse(200, "Detalhes da consulta retornados com sucesso")]
        [SwaggerResponse(401, "Não autorizado")]
        [SwaggerResponse(404, "Consulta não encontrada")]
        [ProducesResponseType(typeof(MedicalAppointmentDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMedicalAppointmentByIdAsync([FromRoute] Guid userId, [FromRoute] Guid appointmentId)
        {
            var medicalAppointment = await _medicalAppointmentAppService.GetMedicalAppointmentByIdAsync(userId, appointmentId, _user);
            return Ok(medicalAppointment);
        }

        [HttpPut("{appointmentId}")]
        [SwaggerOperation(
            Summary = "Atualiza uma consulta médica",
            Description = "Atualiza os dados de uma consulta médica existente"
        )]
        [SwaggerResponse(200, "Consulta atualizada com sucesso")]
        [SwaggerResponse(400, "Dados inválidos fornecidos")]
        [SwaggerResponse(401, "Não autorizado")]
        [SwaggerResponse(404, "Consulta não encontrada")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateMedicalAppointmentAsync([FromRoute] Guid userId,
            [FromRoute] Guid appointmentId,
            [FromBody] UpdateMedicalAppointmentDto updateMedicalAppointmentDto)
        {
            await _medicalAppointmentAppService.UpdateMedicalAppointmentAsync(userId, appointmentId, updateMedicalAppointmentDto, _user);
            return Ok();
        }

        [HttpDelete("{appointmentId}")]
        [SwaggerOperation(
            Summary = "Remove uma consulta médica",
            Description = "Deleta uma consulta médica do sistema"
        )]
        [SwaggerResponse(200, "Consulta deletada com sucesso")]
        [SwaggerResponse(401, "Não autorizado")]
        [SwaggerResponse(404, "Consulta não encontrada")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteMedicalAppointmentAsync([FromRoute] Guid userId, [FromRoute] Guid appointmentId)
        {
            await _medicalAppointmentAppService.DeleteMedicalAppointmentAsync(userId, appointmentId, _user);
            return NoContent();
        }
    }
}
