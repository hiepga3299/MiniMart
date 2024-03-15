using MiniMart.Domain.Entities;

namespace MiniMart.Infatructure.Abstract
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetListProduct();
    }
}