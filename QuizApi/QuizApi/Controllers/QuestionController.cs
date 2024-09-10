using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using QuizApi.Data.Repositories;
using QuizApi.Dtos;
using QuizApi.Models;
using QuizApi.UseCase;

namespace QuizApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IUpdateQuestionUseState _updateQuestionUseState;
        private readonly IGetQuestionUseCase _getQuestionUseCase;

        public QuestionController(IQuestionRepository questionRepository, IUpdateQuestionUseState updateQuestionUseState, IGetQuestionUseCase getQuestionUseCase)
        {
            _questionRepository = questionRepository;
            _updateQuestionUseState = updateQuestionUseState;
            _getQuestionUseCase = getQuestionUseCase;
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(QuestionDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(QuestionDto))]
        public async Task<IActionResult> GetQuestion(int id)
        {
            try
            {
                Question question = await _getQuestionUseCase.GetQuestionAsync(id);

                return Ok(question.ToDto());
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("random")]
        public async Task<IActionResult> GetRandomQuestion()
        {
            try
            {
                Question question = await _getQuestionUseCase.GetRandomQuestionAsync();
                return Ok(question.ToDto());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("random/category/{categoryId}")]
        public async Task<IActionResult> GetRandomQuestionByCategory(int categoryId)
        {
            var question = await _getQuestionUseCase.GetRandomQuestionByCategoryAsync(categoryId);
            if (question == null)
            {
                return NotFound();
            }
            return Ok(question);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(QuestionDto))]
        public async Task<IActionResult> GetAllQuestions()
        {
            List<Question> result = await _questionRepository.GetAllQuestionsAsync();

            return new OkObjectResult(result.Select(c => c.ToDto()).ToList());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(QuestionDto))]
        public async Task<IActionResult> CreateQuestion(CreateQuestionDto question)
        {
            Question result = await _questionRepository.CreateQuestionAsync(question);
            return new CreatedResult($"https://localhost:7221/api/question/{result.Id}", null);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(QuestionDto))]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            var result = await _questionRepository.DeleteQuestionAsync(id);
            return new OkObjectResult(result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(QuestionDto))]
        public async Task<IActionResult> UpdateQuestion(QuestionDto question)
        {
            QuestionDto? result = await _updateQuestionUseState.Execute(question);
            if (result == null)
                return new NotFoundResult();

            return new OkObjectResult(result);
        }
    }
}
