using MiniMart.Application.DTOs;
using MiniMart.Domain.Entities;

namespace MiniMart.Infatructure.Abstract
{
    public interface ICategoryRepository
    {
        Task CreateCategoryAsync(Category category);
        bool DeleteCategory(Category category);
        Task<Category> GetById(int id);
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<IEnumerable<Category>> GetCategoriesForDataTableAsync(RequestDataTableModel requestData);
        Task UpdateCategoryAsync(Category category);
    }
}