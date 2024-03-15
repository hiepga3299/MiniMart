using MiniMart.Domain.Entities;

namespace MiniMart.Infatructure.Abstract
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProduct();
    }
}
