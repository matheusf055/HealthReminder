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
    [Route("api/appointments")]
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
            Summary = "Criar nova consulta médica",
            Description = "Agenda uma nova consulta médica no sistema"
        )]
        [SwaggerResponse(201, "Consulta médica criada com sucesso", typeof(MedicalAppointmentDto))]
        [SwaggerResponse(400, "Requisição inválida - Dados da consulta inválidos")]
        [SwaggerResponse(401, "Não autorizado - Usuário não autenticado")]
        [ProducesResponseType(typeof(MedicalAppointmentDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create([FromBody] CreateMedicalAppointmentCommand command)
        {
            var result = await _medicalAppointmentAppService.Create(command, _user);
            return Ok(result);
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Listar consultas médicas",
            Description = "Retorna todas as consultas médicas do usuário"
        )]
        [SwaggerResponse(200, "Lista de consultas médicas recuperada com sucesso", typeof(IEnumerable<MedicalAppointmentDto>))]
        [SwaggerResponse(401, "Não autorizado - Usuário não autenticado")]
        [ProducesResponseType(typeof(IEnumerable<MedicalAppointmentDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAll()
        {
            var medicalAppointments = await _medicalAppointmentAppService.GetAll(_user);
            return Ok(medicalAppointments);
        }

        [HttpGet("{appointmentId}")]
        [SwaggerOperation(
            Summary = "Obter consulta médica por ID",
            Description = "Retorna os detalhes de uma consulta médica específica"
        )]
        [SwaggerResponse(200, "Consulta médica encontrada com sucesso", typeof(MedicalAppointmentDto))]
        [SwaggerResponse(401, "Não autorizado - Usuário não autenticado")]
        [SwaggerResponse(404, "Não encontrado - Consulta médica não existe")]
        [ProducesResponseType(typeof(MedicalAppointmentDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] Guid appointmentId)
        {
            var medicalAppointment = await _medicalAppointmentAppService.GetById(appointmentId, _user);
            return Ok(medicalAppointment);
        }

        [HttpPut]
        [SwaggerOperation(
            Summary = "Atualizar consulta médica",
            Description = "Atualiza as informações de uma consulta médica existente"
        )]
        [SwaggerResponse(200, "Consulta médica atualizada com sucesso", typeof(MedicalAppointmentDto))]
        [SwaggerResponse(400, "Requisição inválida - Dados da consulta inválidos")]
        [SwaggerResponse(401, "Não autorizado - Usuário não autenticado")]
        [SwaggerResponse(404, "Não encontrado - Consulta médica não existe")]
        [ProducesResponseType(typeof(MedicalAppointmentDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody] UpdateMedicalAppointmentCommand command)
        {
            await _medicalAppointmentAppService.Update(command, _user);
            return Ok();
        }

        [HttpDelete("{appointmentId}")]
        [SwaggerOperation(
            Summary = "Excluir consulta médica",
            Description = "Remove uma consulta médica do sistema"
        )]
        [SwaggerResponse(204, "Consulta médica excluída com sucesso")]
        [SwaggerResponse(401, "Não autorizado - Usuário não autenticado")]
        [SwaggerResponse(404, "Não encontrado - Consulta médica não existe")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] Guid appointmentId)
        {
            await _medicalAppointmentAppService.Delete(appointmentId, _user);
            return NoContent();
        }
    }
}
