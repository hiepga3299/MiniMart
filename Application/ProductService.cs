using MiniMart.Domain.Entities;
using MiniMart.Infatructure.Abstract;

namespace MiniMart.Application
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Product>> GetListProduct()
        {
            var data = await _unitOfWork.ProductRepository.GetAllProduct();
            return data.Select(x => new Product { Name = x.Name, Price = x.Price });
        }
    }
}
