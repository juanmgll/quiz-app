using QuizApi.Dtos;

namespace QuizApi.Models
{
    public class Category
    {
        public int? Id { get; set; }
        public string CategoryName { get; set; }

        public CategoryDto ToDto()
        {
            return new CategoryDto()
            {
                Id = Id ?? throw new Exception("Could not be saved!"),
                CategoryName = CategoryName

            };
        }
    }
}
