using QuizApi.Data;
using QuizApi.Data.Repositories;
using QuizApi.Dtos;

namespace QuizApi.UseCase
{
    public interface IUpdateCategoryUseCase
    {
        Task<CategoryDto?> Execute(CategoryDto category);
    }

    public class UpdateCategoryUseCase : IUpdateCategoryUseCase
    {
        private readonly ICategoryRepository _categoryRepository;

        public UpdateCategoryUseCase(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryDto?> Execute(CategoryDto category)
        {
            var entity = await _categoryRepository.GetCategoryAsync(category.Id);
            if (entity == null)
                return null;

            entity.CategoryName = category.CategoryName;

            await _categoryRepository.UpdateCategoryAsync(entity);
            return entity.ToDto();
        }
    }

}
