using MiniMart.Domain.Entities;
using MiniMart.Infatructure.Abstract;
using MiniMart.Infatructure.DataAccess;

namespace MiniMart.Infatructure.Repository
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(MiniMartDbContext context) : base(context)
        {

        }
        public async Task<IEnumerable<Product>> GetAllProduct()
        {
            return await GetAllAsync();
        }
    }
}
