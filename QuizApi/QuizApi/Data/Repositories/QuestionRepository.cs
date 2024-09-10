using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using QuizApi.Dtos;
using QuizApi.Models;

namespace QuizApi.Data.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        public readonly QuizDbContext _context;

        public QuestionRepository(QuizDbContext context)
        {
            _context = context;
        }

        public async Task<Question> GetQuestionAsync(int id)
        {
            return await _context.Question
                         .Include(q => q.Answers)
                         .FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<Question> GetRandomQuestionAsync()
        {
            int count = await _context.Question.CountAsync();
            int index = new Random().Next(count);
            return await _context.Question
                .Include(q => q.Answers)
                .Skip(index)
                .FirstOrDefaultAsync();
        }

        public async Task<Question> GetRandomQuestionByCategoryAsync(int categoryId)
        {
            return await _context.Question
            .Include(q => q.Answers)
            .Where(q => q.CategoryId == categoryId)
            .OrderBy(q => Guid.NewGuid())
            .FirstOrDefaultAsync();
        }

        public async Task<List<Question>> GetAllQuestionsAsync()
        {
            return await _context.Question.Include(q => q.Answers).ToListAsync();
        }

        public async Task<Question> CreateQuestionAsync(CreateQuestionDto question)
        {
            Question entity = new Question()
            {
                Id = null,
                CategoryId = question.CategoryId,
                Title = question.Title
            };

            EntityEntry<Question> response = await _context.Question.AddAsync(entity);
            await _context.SaveChangesAsync();

            return await GetQuestionAsync(response.Entity.Id ?? throw new Exception("Could not be saved"));
        }

        public async Task<bool> DeleteQuestionAsync(int id)
        {
            Question entity = await GetQuestionAsync(id);

            if (entity == null)
            {
                throw new Exception("Question not found");
            }

            _context.Question.Remove(entity);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateQuestionAsync(Question question)
        {
            _context.Question.Update(question);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
