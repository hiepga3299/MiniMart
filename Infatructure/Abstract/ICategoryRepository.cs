using MiniMart.Domain.Entities;

namespace MiniMart.Infatructure.Abstract
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();
    }
}