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
        public async Task CreateCategoryAsync(Category category)
        {
            await Create(category);
            Commit();
        }

        public async Task<Category> GetById(int id)
        {
            return await GetSingleAsync(x => x.Id == id);
        }

        public void UpdateCategory(Category category)
        {
            Update(category);
            Commit();
        }
    }
}
