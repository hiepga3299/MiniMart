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
    }
}
