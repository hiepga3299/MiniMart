using MiniMart.Application.DTOs;
using MiniMart.Domain.Entities;
using MiniMart.Infatructure.Abstract;
using MiniMart.Infatructure.DataAccess;

namespace MiniMart.Infatructure.Repository
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(MiniMartDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await GetAllAsync();
        }

        public async Task<IEnumerable<Category>> GetCategoriesForDataTableAsync(RequestDataTableModel requestData)
        {
            return await GetAllAsync(x => string.IsNullOrEmpty(requestData.Keyword) || (x.Name.Contains(requestData.Keyword)));
        }
        public async Task CreateCategoryAsync(Category category)
        {
            await Create(category);
        }

        public async Task<Category> GetById(int id)
        {
            return await GetSingleAsync(x => x.Id == id);
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            await Update(category);
            Commit();
        }

        public bool DeleteCategory(Category category)
        {
            if (category.Id != 0)
            {
                base.Delete(category);
            }
            return true;
        }
    }
}
