using QuizApi.Dtos;
using QuizApi.Models;

namespace QuizApi.Data.Repositories
{
    public interface IQuestionRepository
    {
        Task<Question?> GetQuestionAsync(int id);
        Task<Question> GetRandomQuestionAsync();
        Task<Question> GetRandomQuestionByCategoryAsync(int categoryId);
        Task<List<Question>> GetAllQuestionsAsync();
        Task<Question> CreateQuestionAsync(CreateQuestionDto question);
        Task<bool> DeleteQuestionAsync(int id);
        Task<bool> UpdateQuestionAsync(Question question);
    }
}
