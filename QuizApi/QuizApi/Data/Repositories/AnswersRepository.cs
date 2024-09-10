using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using QuizApi.Dtos;
using QuizApi.Models;

namespace QuizApi.Data.Repositories
{
    public class AnswersRepository : IAnswerRepository
    {
        private readonly QuizDbContext _context;
        public AnswersRepository(QuizDbContext context)
        {
            _context = context;
        }

        public async Task<Answers?> GetAnswersAsync(int id)
        {
            return await _context.Answer.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Answers>> GetAllAnswersAsync()
        {
            return await _context.Answer.ToListAsync();
        }
        public async Task<bool> DeleteAnswersAsync(int id)
        {
            Answers result = await GetAnswersAsync(id);
            if (result == null)
                throw new Exception("Answers not found");

            _context.Answer.Remove(result);
            _context.SaveChangesAsync();

            return true;
        }

        public async Task<Answers> CreateAnswersAsync(CreateAnswersDto answers)
        {
            Answers entity = new Answers()
            {
                AnswerText = answers.AnswerText,
                IsCorrect = answers.IsCorrect,
                QuestionId = answers.QuestionId
            };

            EntityEntry<Answers> response = await _context.Answer.AddAsync(entity);
            await _context.SaveChangesAsync();

            return await GetAnswersAsync(response.Entity.Id ?? throw new Exception("Could not saved"));
        }

        public async Task<bool> UpdateAnswersAsync(Answers answers)
        {
            _context.Answer.Update(answers);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
