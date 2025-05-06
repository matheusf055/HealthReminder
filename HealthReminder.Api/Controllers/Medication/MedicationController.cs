using HealthReminder.AppService.Interfaces.Medication;
using HealthReminder.AppService.Medication.DTOs;
using HealthReminder.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;
using HealthReminder.AppService.Medication.Commands;

namespace HealthReminder.Api.Controllers.Medication
{
    [ApiController]
    [Authorize]
    [Route("api/medications")]
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
        [SwaggerOperation(
            Summary = "Adiciona uma nova medicação",
            Description = "Cria um novo registro de medicação para o usuário"
        )]
        [SwaggerResponse(200, "Medicação criada com sucesso")]
        [SwaggerResponse(400, "Dados inválidos fornecidos")]
        [SwaggerResponse(401, "Não autorizado")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create([FromBody] CreateMedicationCommand command)
        {
            var result = await _medicationAppService.Create(command, _user);
            return Ok(result);
        }

        [HttpPost("{medicationId}/take")]
        [SwaggerOperation(
            Summary = "Registra a tomada de uma medicação",
            Description = "Marca uma medicação como tomada pelo usuário"
        )]
        [SwaggerResponse(200, "Medicação registrada como tomada")]
        [SwaggerResponse(401, "Não autorizado")]
        [SwaggerResponse(404, "Medicação não encontrada")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> TakeMedication([FromRoute] Guid medicationId)
        {
            await _medicationAppService.Take(medicationId, _user);
            return Ok();
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Lista todas as medicações do usuário",
            Description = "Retorna todas as medicações cadastradas para o usuário"
        )]
        [SwaggerResponse(200, "Lista de medicações retornada com sucesso")]
        [SwaggerResponse(401, "Não autorizado")]
        [ProducesResponseType(typeof(IEnumerable<MedicationDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAll()
        {
            var medications = await _medicationAppService.GetAll(_user);
            return Ok(medications);
        }

        [HttpGet("{medicationId}")]
        [SwaggerOperation(
            Summary = "Obtém detalhes de uma medicação específica",
            Description = "Retorna os detalhes de uma medicação específica pelo ID"
        )]
        [SwaggerResponse(200, "Detalhes da medicação retornados com sucesso")]
        [SwaggerResponse(401, "Não autorizado")]
        [SwaggerResponse(404, "Medicação não encontrada")]
        [ProducesResponseType(typeof(MedicationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] Guid medicationId)
        {
            var medication = await _medicationAppService.GetById(medicationId, _user);
            return Ok(medication);
        }

        [HttpPut("{medicationId}")]
        [SwaggerOperation(
            Summary = "Atualiza uma medicação",
            Description = "Atualiza os dados de uma medicação existente"
        )]
        [SwaggerResponse(200, "Medicação atualizada com sucesso")]
        [SwaggerResponse(400, "Dados inválidos fornecidos")]
        [SwaggerResponse(401, "Não autorizado")]
        [SwaggerResponse(404, "Medicação não encontrada")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] Guid medicationId, [FromBody] UpdateMedicationCommand command)
        {
            command.Id = medicationId;
            await _medicationAppService.Update(command, _user);
            return Ok();
        }

        [HttpDelete("{medicationId}")]
        [SwaggerOperation(
            Summary = "Remove uma medicação",
            Description = "Deleta uma medicação do sistema"
        )]
        [SwaggerResponse(200, "Medicação deletada com sucesso")]
        [SwaggerResponse(401, "Não autorizado")]
        [SwaggerResponse(404, "Medicação não encontrada")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] Guid medicationId)
        {
            await _medicationAppService.Delete(medicationId, _user);
            return NoContent();
        }
    }
}
