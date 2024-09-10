using System.ComponentModel.DataAnnotations;

namespace QuizApi.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
    }
}
