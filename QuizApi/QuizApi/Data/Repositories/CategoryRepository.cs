using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using QuizApi.Dtos;
using QuizApi.Models;

namespace QuizApi.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly QuizDbContext _context;
        public CategoryRepository(QuizDbContext context)
        {
            _context = context;
        }

        public async Task<Category?> GetCategoryAsync(int id)
        {
            return await _context.Category.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _context.Category.ToListAsync();
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            Category entity = await GetCategoryAsync(id);

            if (entity == null)
                throw new  Exception("Categore not found");

            _context.Category.Remove(entity);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Category> CreateCategoryAsync(CreateCategoryDto category)
        {
            Category entity = new Category()
            {
                Id = null,
                CategoryName = category.CategoryName
            };

            EntityEntry<Category> response = await _context.Category.AddAsync(entity);
            await _context.SaveChangesAsync();

            return await GetCategoryAsync(response.Entity.Id ?? throw new Exception("Could not saved"));
        }

        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            _context.Category.Update(category);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
