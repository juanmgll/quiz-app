using QuizApi.Models;

namespace QuizApi.Dtos
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public  int CategoryId { get; set; }
        public string Title { get; set; }
        public List<AnswersDto> Answers { get; set; }

    }
}
