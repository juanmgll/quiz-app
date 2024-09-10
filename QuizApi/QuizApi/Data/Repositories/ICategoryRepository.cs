using QuizApi.Dtos;
using QuizApi.Models;

namespace QuizApi.Data.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category?> GetCategoryAsync(int id);
        Task<List<Category>> GetAllCategoriesAsync();
        Task<bool> DeleteCategoryAsync(int id);
        Task<Category> CreateCategoryAsync(CreateCategoryDto category);
        Task<bool> UpdateCategoryAsync(Category category);
    }
}
