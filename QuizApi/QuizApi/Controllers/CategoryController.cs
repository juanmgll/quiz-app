using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using QuizApi.Data.Repositories;
using QuizApi.Dtos;
using QuizApi.Models;
using QuizApi.UseCase;

namespace QuizApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUpdateCategoryUseCase _updateCategoryUseCase;

        public CategoryController(ICategoryRepository categoryRepository, IUpdateCategoryUseCase updateCategoryUseCase)
        {
            _categoryRepository = categoryRepository;
            _updateCategoryUseCase = updateCategoryUseCase;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CategoryDto))]
        public async Task<IActionResult> GetCategory(int id)
        {
            Category result = await _categoryRepository.GetCategoryAsync(id);
            if (result == null) return NotFound();

            return new OkObjectResult(result.ToDto());
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDto))]
        public async Task<IActionResult> GetAllCategories()
        {
            List<Category> result = await _categoryRepository.GetAllCategoriesAsync();

            return new OkObjectResult(result.Select(c => c.ToDto()).ToList());
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDto))]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _categoryRepository.DeleteCategoryAsync(id);

            return new OkObjectResult(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDto))]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto category)
        {
            Category result = await _categoryRepository.CreateCategoryAsync(category);
            return new CreatedResult($"https://localhost:7221/api/category/{result.Id}", null);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDto))]
        public async Task<IActionResult> UpdateCategory(CategoryDto category)
        {
            CategoryDto? result = await _updateCategoryUseCase.Execute(category);
            if(result == null)
                return new NotFoundResult();

            return new OkObjectResult(result);
        }
    }
}
