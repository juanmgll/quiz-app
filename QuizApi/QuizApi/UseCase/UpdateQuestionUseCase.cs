using QuizApi.Data.Repositories;
using QuizApi.Dtos;

namespace QuizApi.UseCase
{
    public interface IUpdateQuestionUseState
    {
        Task<QuestionDto?> Execute(QuestionDto question);
    }
    public class UpdateQuestionUseCase: IUpdateQuestionUseState
    {
        private readonly IQuestionRepository _questionRepository;

        public UpdateQuestionUseCase(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<QuestionDto?> Execute(QuestionDto question)
        {
            var entity = await _questionRepository.GetQuestionAsync(question.Id);
            if (entity == null)
                return null;

            entity.CategoryId = question.CategoryId;
            entity.Title = question.Title;

            await _questionRepository.UpdateQuestionAsync(entity);

            return entity.ToDto();
        }
    }
}
