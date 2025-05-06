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
            Summary = "Criar novo exame",
            Description = "Cadastra um novo exame no sistema"
        )]
        [SwaggerResponse(201, "Exame criado com sucesso", typeof(ExamDto))]
        [SwaggerResponse(400, "Requisição inválida - Dados do exame inválidos")]
        [SwaggerResponse(401, "Não autorizado - Usuário não autenticado")]
        [ProducesResponseType(typeof(ExamDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create([FromBody] CreateExamCommand command)
        {
            var result = await _examAppService.Create(command, _user);
            return Ok(result);
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Listar exames",
            Description = "Retorna a lista de todos os exames cadastrados para o usuário"
        )]
        [SwaggerResponse(200, "Lista de exames recuperada com sucesso", typeof(IEnumerable<ExamDto>))]
        [SwaggerResponse(401, "Não autorizado - Usuário não autenticado")]
        [ProducesResponseType(typeof(IEnumerable<ExamDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAll()
        {
            var exams = await _examAppService.GetAll(_user);
            return Ok(exams);
        }

        [HttpGet("{examId}")]
        [SwaggerOperation(
            Summary = "Obter exame por ID",
            Description = "Retorna os detalhes de um exame específico"
        )]
        [SwaggerResponse(200, "Exame encontrado com sucesso", typeof(ExamDto))]
        [SwaggerResponse(401, "Não autorizado - Usuário não autenticado")]
        [SwaggerResponse(404, "Não encontrado - Exame não existe")]
        [ProducesResponseType(typeof(ExamDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] Guid examId)
        {
            var exam = await _examAppService.GetById(examId, _user);
            return Ok(exam);
        }

        [HttpPut]
        [SwaggerOperation(
            Summary = "Atualizar exame",
            Description = "Atualiza as informações de um exame existente"
        )]
        [SwaggerResponse(200, "Exame atualizado com sucesso", typeof(ExamDto))]
        [SwaggerResponse(400, "Requisição inválida - Dados do exame inválidos")]
        [SwaggerResponse(401, "Não autorizado - Usuário não autenticado")]
        [SwaggerResponse(404, "Não encontrado - Exame não existe")]
        [ProducesResponseType(typeof(ExamDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(UpdateExamCommand command)
        {
            await _examAppService.Update(command, _user);
            return Ok();
        }

        [HttpDelete("{examId}")]
        [SwaggerOperation(
            Summary = "Excluir exame",
            Description = "Remove um exame do sistema"
        )]
        [SwaggerResponse(204, "Exame excluído com sucesso")]
        [SwaggerResponse(401, "Não autorizado - Usuário não autenticado")]
        [SwaggerResponse(404, "Não encontrado - Exame não existe")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] Guid examId)
        {
            await _examAppService.Delete(examId, _user);
            return NoContent();
        }
    }
}
