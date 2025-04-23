using HealthReminder.AppService.Interfaces.Medication;
using HealthReminder.AppService.Medication.DTOs;
using HealthReminder.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;

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
        public async Task<IActionResult> AddMedicationAsync([FromRoute] Guid userId, [FromBody] CreateMedicationDto createMedicationDto)
        {
            await _medicationAppService.AddMedicationAsync(userId, createMedicationDto, _user);
            return Ok("Medicação criada com sucesso.");
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
        public async Task<IActionResult> TakeMedication([FromRoute] Guid userId, [FromRoute] Guid medicationId)
        {
            await _medicationAppService.TakeMedicationAsync(userId, medicationId, _user);
            return Ok("Medicação registrada como tomada.");
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
        public async Task<IActionResult> GetMedicationsByUserIdAsync([FromRoute] Guid userId)
        {
            var medications = await _medicationAppService.GetMedicationsByUserIdAsync(userId, _user);
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
        public async Task<IActionResult> GetMedicationByIdAsync([FromRoute] Guid userId, [FromRoute] Guid medicationId)
        {
            var medication = await _medicationAppService.GetMedicationByIdAsync(userId, medicationId, _user);
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
        public async Task<IActionResult> UpdateMedicationAsync([FromRoute] Guid userId,
            [FromRoute] Guid medicationId,
            [FromBody] UpdateMedicationDto updateMedicationDto)
        {
            await _medicationAppService.UpdateMedicationAsync(userId, medicationId, updateMedicationDto, _user);
            return Ok("Medicação atualizada com sucesso.");
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
        public async Task<IActionResult> DeleteMedicationAsync(
            [FromRoute] Guid userId,
            [FromRoute] Guid medicationId)
        {
            await _medicationAppService.DeleteMedicationAsync(userId, medicationId, _user);
            return Ok("Medicação deletada com sucesso.");
        }
    }
}
