using MiniMart.Domain.Entities;

namespace MiniMart.Infatructure.Abstract
{
    public interface IProductRepository
    {
        Task<(IEnumerable<T>, int)> GetAllProductPagination<T>(string keywork, int pageIndex, int pageSize);
        Task<IEnumerable<T>> GetCategoryByProductId<T>(int? id);
        Task<Product> GetProductByCode(string? code);
        Task<Product> GetSingleProduct(int? id);
        Task<bool?> SaveProduct(Product product);
    }
}
