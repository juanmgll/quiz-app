using System.ComponentModel.DataAnnotations;

namespace QuizApi.Dtos
{
    public class CreateAnswersDto
    {
        [Required(ErrorMessage = "Answer text is required")]
        public string AnswerText { get; set; }

        [Required(ErrorMessage = "IsCorrect (0: false 1: true) is required")]
        public bool IsCorrect { get; set; }
        [Required(ErrorMessage = "QuestionId is required")]
        public int QuestionId { get; set; }
    }
}
