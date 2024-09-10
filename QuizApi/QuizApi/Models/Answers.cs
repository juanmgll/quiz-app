using QuizApi.Dtos;

namespace QuizApi.Models
{
    public class Answers
    {
        public int? Id { get; set; }
        public string AnswerText { get; set; }
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }

        public AnswersDto ToDto()
        {
            return new AnswersDto()
            {
                Id = Id ?? throw new Exception("Id not found"),
                AnswerText = AnswerText,
                IsCorrect = IsCorrect,
                QuestionId = QuestionId
            };
        }
    }
}
