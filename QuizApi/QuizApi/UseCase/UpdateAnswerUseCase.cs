using QuizApi.Data.Repositories;
using QuizApi.Dtos;
using QuizApi.Models;

namespace QuizApi.UseCase
{
    public interface IUpdateAnswerUseCase
    {
        Task<AnswersDto?> Execute(AnswersDto answer);
    }

    public class UpdateAnswerUseCase : IUpdateAnswerUseCase
    {
        private readonly IAnswerRepository _answerRepository;

        public UpdateAnswerUseCase(IAnswerRepository answerRepository)
        {
            _answerRepository = answerRepository;
        }

        public async Task<AnswersDto?> Execute(AnswersDto answer)
        {
            var entity = await _answerRepository.GetAnswersAsync(answer.Id);
            if (entity == null)
                return null;

            entity.AnswerText = answer.AnswerText;
            entity.IsCorrect = answer.IsCorrect;
            entity.QuestionId = answer.QuestionId;

            await _answerRepository.UpdateAnswersAsync(entity);
            return entity.ToDto();
        }
    }
}
