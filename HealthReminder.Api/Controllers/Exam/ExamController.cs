using HealthReminder.AppService.Exam.DTOs;
using HealthReminder.AppService.Interfaces.Exam;
using HealthReminder.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthReminder.Api.Controllers.Exam
{
    [ApiController]
    [Authorize]
    [Route("api/exam")]
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
        public async Task<IActionResult> AddExamAsync(CreateExamDto createExamDto)
        {
            await _examAppService.AddExamAsync(createExamDto, _user);
            return Ok("Exame criado com sucesso.");
        }

        [HttpGet]
        public async Task<IActionResult> GetExamsByUserIdAsync()
        {
            var exams = await _examAppService.GetExamsByUserIdAsync(_user.Id, _user);
            return Ok(exams);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExamByIdAsync(Guid id)
        {
            var exam = await _examAppService.GetExamByIdAsync(id, _user);
            return Ok(exam);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExamAsync(Guid id, UpdateExamDto updateExamDto)
        {
            await _examAppService.UpdateExamAsync(id, updateExamDto, _user);
            return Ok("Exame atualizado com sucesso.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExamAsync(Guid id)
        {
            await _examAppService.DeleteExamByIdAsync(id, _user.Id, _user);
            return Ok("Exame excluído com sucesso.");
        }
    }
}
