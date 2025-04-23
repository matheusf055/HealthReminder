using HealthReminder.AppService.Exam.DTOs;
using HealthReminder.AppService.Interfaces.Exam;
using HealthReminder.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthReminder.Api.Controllers.Exam
{
    [ApiController]
    [Authorize]
    [Route("api/{userId}/exams")]
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
        public async Task<IActionResult> AddExamAsync([FromRoute] Guid userId, [FromBody] CreateExamDto createExamDto)
        {
            await _examAppService.AddExamAsync(userId, createExamDto, _user);
            return Ok("Exame criado com sucesso.");
        }

        [HttpGet]
        public async Task<IActionResult> GetExamsByUserIdAsync([FromRoute] Guid userId)
        {
            var exams = await _examAppService.GetExamsByUserIdAsync(userId, _user);
            return Ok(exams);
        }

        [HttpGet("{examId}")]
        public async Task<IActionResult> GetExamByIdAsync([FromRoute] Guid userId, [FromRoute] Guid examId)
        {
            var exam = await _examAppService.GetExamByIdAsync(userId, examId, _user);
            return Ok(exam);
        }

        [HttpPut("{examId}")]
        public async Task<IActionResult> UpdateExamAsync([FromRoute] Guid userId, [FromRoute] Guid examId, [FromBody] UpdateExamDto updateExamDto)
        {
            await _examAppService.UpdateExamAsync(userId, examId, updateExamDto, _user);
            return Ok("Exame atualizado com sucesso.");
        }

        [HttpDelete("{examId}")]
        public async Task<IActionResult> DeleteExamAsync([FromRoute] Guid userId, [FromRoute] Guid examId)
        {
            await _examAppService.DeleteExamByIdAsync(userId, examId, _user);
            return Ok("Exame excluído com sucesso.");
        }
    }
}
