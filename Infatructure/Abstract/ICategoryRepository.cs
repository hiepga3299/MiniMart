using MiniMart.Domain.Entities;

namespace MiniMart.Infatructure.Abstract
{
    public interface ICategoryRepository
    {
        Task CreateCategoryAsync(Category category);
        Task<Category> GetById(int id);
        Task<IEnumerable<Category>> GetCategoriesAsync();
        void UpdateCategory(Category category);
    }
}