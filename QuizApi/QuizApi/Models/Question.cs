using QuizApi.Dtos;

namespace QuizApi.Models
{
    public class Question
    {
        public int? Id { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public List<Answers> Answers { get; set; } = new List<Answers>();


        public QuestionDto ToDto()
        {
            return new QuestionDto()
            {
                Id = Id ?? throw new Exception("Could not be saved"),
                CategoryId = CategoryId,
                Title = Title,
                Answers = Answers.Select(a => a.ToDto()).ToList() // Convert each answer into a dto
            };
        }
    }
}
