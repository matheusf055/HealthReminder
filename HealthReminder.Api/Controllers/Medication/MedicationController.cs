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
            Summary = "Criar novo medicamento",
            Description = "Cadastra um novo medicamento no sistema"
        )]
        [SwaggerResponse(201, "Medicamento criado com sucesso", typeof(MedicationDto))]
        [SwaggerResponse(400, "Requisição inválida - Dados do medicamento inválidos")]
        [SwaggerResponse(401, "Não autorizado - Usuário não autenticado")]
        [ProducesResponseType(typeof(MedicationDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
            Summary = "Listar medicamentos",
            Description = "Retorna a lista de todos os medicamentos cadastrados para o usuário"
        )]
        [SwaggerResponse(200, "Lista de medicamentos recuperada com sucesso", typeof(IEnumerable<MedicationDto>))]
        [SwaggerResponse(401, "Não autorizado - Usuário não autenticado")]
        [ProducesResponseType(typeof(IEnumerable<MedicationDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAll()
        {
            var medications = await _medicationAppService.GetAll(_user);
            return Ok(medications);
        }

        [HttpGet("{medicationId}")]
        [SwaggerOperation(
            Summary = "Obter medicamento por ID",
            Description = "Retorna os detalhes de um medicamento específico"
        )]
        [SwaggerResponse(200, "Medicamento encontrado com sucesso", typeof(MedicationDto))]
        [SwaggerResponse(401, "Não autorizado - Usuário não autenticado")]
        [SwaggerResponse(404, "Não encontrado - Medicamento não existe")]
        [ProducesResponseType(typeof(MedicationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] Guid medicationId)
        {
            var medication = await _medicationAppService.GetById(medicationId, _user);
            return Ok(medication);
        }

        [HttpPut]
        [SwaggerOperation(
            Summary = "Atualizar medicamento",
            Description = "Atualiza as informações de um medicamento existente"
        )]
        [SwaggerResponse(200, "Medicamento atualizado com sucesso", typeof(MedicationDto))]
        [SwaggerResponse(400, "Requisição inválida - Dados do medicamento inválidos")]
        [SwaggerResponse(401, "Não autorizado - Usuário não autenticado")]
        [SwaggerResponse(404, "Não encontrado - Medicamento não existe")]
        [ProducesResponseType(typeof(MedicationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody] UpdateMedicationCommand command)
        {
            await _medicationAppService.Update(command, _user);
            return Ok();
        }

        [HttpDelete("{medicationId}")]
        [SwaggerOperation(
            Summary = "Excluir medicamento",
            Description = "Remove um medicamento do sistema"
        )]
        [SwaggerResponse(204, "Medicamento excluído com sucesso")]
        [SwaggerResponse(401, "Não autorizado - Usuário não autenticado")]
        [SwaggerResponse(404, "Não encontrado - Medicamento não existe")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] Guid medicationId)
        {
            await _medicationAppService.Delete(medicationId, _user);
            return NoContent();
        }
    }
}
