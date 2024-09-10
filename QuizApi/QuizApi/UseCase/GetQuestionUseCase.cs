using Microsoft.EntityFrameworkCore;
using QuizApi.Data.Repositories;
using QuizApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApi.UseCase
{
    public interface IGetQuestionUseCase
    {
        Task<Question> GetQuestionAsync(int id);
        Task<Question> GetRandomQuestionAsync();
        Task<Question> GetRandomQuestionByCategoryAsync(int categoryId);
    }

    public class GetQuestionUseCase : IGetQuestionUseCase
    {
        private readonly IQuestionRepository _questionRepository;

        public GetQuestionUseCase(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<Question> GetQuestionAsync(int id)
        {
            var question = await _questionRepository.GetQuestionAsync(id);
            return RandomizeAnswers(question);
        }

        public async Task<Question> GetRandomQuestionAsync()
        {
            var question = await _questionRepository.GetRandomQuestionAsync();

            return RandomizeAnswers(question);
        }

        public async Task<Question> GetRandomQuestionByCategoryAsync(int categoryId)
        {
            var question = await _questionRepository.GetRandomQuestionByCategoryAsync(categoryId);
            return RandomizeAnswers(question);
        }

        private Question RandomizeAnswers(Question question)
        {
            if (question == null)
            {
                throw new KeyNotFoundException("Question not found");
            }

            var correctAnswers = question.Answers.Where(a => a.IsCorrect).ToList();
            var incorrectAnswers = question.Answers.Where(a => !a.IsCorrect).ToList();

            incorrectAnswers = incorrectAnswers.OrderBy(a => Guid.NewGuid()).ToList();

            var selectedAnswers = new List<Answers>();

            if (correctAnswers.Any())
            {
                selectedAnswers.Add(correctAnswers.First());
            }

            selectedAnswers.AddRange(incorrectAnswers.Take(4 - selectedAnswers.Count));

            question.Answers = selectedAnswers.OrderBy(a => Guid.NewGuid()).ToList();

            return question;
        }
    }
}
