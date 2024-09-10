using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizApi.Data.Repositories;
using QuizApi.Dtos;
using QuizApi.Models;
using QuizApi.UseCase;

namespace QuizApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerRepository _answersRepository;
        private readonly IUpdateAnswerUseCase _updateAnswerUseCase;

        public AnswerController(IAnswerRepository answersRepository, IUpdateAnswerUseCase updateAnswerUseCase)
        {
            _answersRepository = answersRepository;
            _updateAnswerUseCase = updateAnswerUseCase;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AnswersDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(AnswersDto))]
        public async Task<IActionResult> GetAnswer(int id)
        {
            Answers result = await _answersRepository.GetAnswersAsync(id);
            if (result == null)
                return NotFound();

            return new ObjectResult(result.ToDto());
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AnswersDto))]
        public async Task<IActionResult> GetAllAnswers()
        {
            List<Answers> result = await _answersRepository.GetAllAnswersAsync();
            return new OkObjectResult(result.Select(c => c.ToDto()).ToList());
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AnswersDto))]
        public async Task<IActionResult> DeleteAnswer(int id)
        {
            var result = await _answersRepository.DeleteAnswersAsync(id);
            return new OkObjectResult(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AnswersDto))]
        public async Task<IActionResult> CreateAnswer(CreateAnswersDto answer)
        {
            Answers result = await _answersRepository.CreateAnswersAsync(answer);
            return new CreatedResult($"https://localhost:7221/api/answer/{result.Id}", null);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AnswersDto))]
        public async Task<IActionResult> UpdateAnswer(AnswersDto answer)
        {
            AnswersDto? result = await _updateAnswerUseCase.Execute(answer);
            if (result == null)
                return new NotFoundResult();

            return new OkObjectResult(result);

        }
    }
}
