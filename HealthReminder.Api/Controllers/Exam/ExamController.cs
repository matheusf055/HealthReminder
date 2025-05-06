using HealthReminder.AppService.Exam.Commands;
using HealthReminder.AppService.Exam.DTOs;
using HealthReminder.AppService.Interfaces.Exam;
using HealthReminder.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HealthReminder.Api.Controllers.Exam
{
    [ApiController]
    [Authorize]
    [Route("api/exams")]
    public class ExamController : ControllerBase
    {
        private readonly IExamAppService _examAppService;
        private readonly IUser _user;
        
        public ExamController(IExamAppService examAppService, IUser user)
        {
            _examAppService = examAppService;
            _user = user;
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Adiciona um novo exame",
            Description = "Cria um novo registro de exame para o usuário"
        )]
        [SwaggerResponse(200, "Exame criado com sucesso")]
        [SwaggerResponse(400, "Dados inválidos fornecidos")]
        [SwaggerResponse(401, "Não autorizado")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create([FromBody] CreateExamCommand command)
        {
            var result = await _examAppService.Create(command, _user);
            return Ok(result);
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Lista todos os exames do usuário",
            Description = "Retorna todos os exames cadastrados para o usuário"
        )]
        [SwaggerResponse(200, "Lista de exames retornada com sucesso")]
        [SwaggerResponse(401, "Não autorizado")]
        [ProducesResponseType(typeof(IEnumerable<ExamDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAll()
        {
            var exams = await _examAppService.GetAll(_user);
            return Ok(exams);
        }

        [HttpGet("{examId}")]
        [SwaggerOperation(
            Summary = "Obtém detalhes de um exame específico",
            Description = "Retorna os detalhes de um exame específico pelo ID"
        )]
        [SwaggerResponse(200, "Detalhes do exame retornados com sucesso")]
        [SwaggerResponse(401, "Não autorizado")]
        [SwaggerResponse(404, "Exame não encontrado")]
        [ProducesResponseType(typeof(ExamDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] Guid examId)
        {
            var exam = await _examAppService.GetById(examId, _user);
            return Ok(exam);
        }

        [HttpPut("{examId}")]
        [SwaggerOperation(
            Summary = "Atualiza um exame",
            Description = "Atualiza os dados de um exame existente"
        )]
        [SwaggerResponse(200, "Exame atualizado com sucesso")]
        [SwaggerResponse(400, "Dados inválidos fornecidos")]
        [SwaggerResponse(401, "Não autorizado")]
        [SwaggerResponse(404, "Exame não encontrado")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(UpdateExamCommand command)
        {
            await _examAppService.Update(command, _user);
            return Ok();
        }

        [HttpDelete("{examId}")]
        [SwaggerOperation(
            Summary = "Remove um exame",
            Description = "Deleta um exame do sistema"
        )]
        [SwaggerResponse(200, "Exame excluído com sucesso")]
        [SwaggerResponse(401, "Não autorizado")]
        [SwaggerResponse(404, "Exame não encontrado")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] Guid examId)
        {
            await _examAppService.Delete(examId, _user);
            return NoContent();
        }
    }
}
