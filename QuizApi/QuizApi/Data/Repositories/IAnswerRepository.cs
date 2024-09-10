using QuizApi.Dtos;
using QuizApi.Models;

namespace QuizApi.Data.Repositories
{
    public interface IAnswerRepository
    {
        Task<Answers?> GetAnswersAsync(int id);
        Task<List<Answers>> GetAllAnswersAsync();
        Task<bool> DeleteAnswersAsync(int id);
        Task<Answers> CreateAnswersAsync(CreateAnswersDto answers);
        Task<bool> UpdateAnswersAsync(Answers answers);
    }
}
