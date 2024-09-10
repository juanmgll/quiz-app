using System.ComponentModel.DataAnnotations;

namespace QuizApi.Dtos
{
    public class CreateQuestionDto
    {
        [Required(ErrorMessage = "The Category id must be specify")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "The title id must be specify")]
        public string Title { get; set; }
    }
}
