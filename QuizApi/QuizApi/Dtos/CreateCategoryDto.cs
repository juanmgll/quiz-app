using System.ComponentModel.DataAnnotations;

namespace QuizApi.Dtos
{
    public class CreateCategoryDto
    {
        [Required(ErrorMessage = "The name must be specify")]
        public string CategoryName { get; set; }
    }
}
