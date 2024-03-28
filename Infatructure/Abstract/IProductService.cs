using MiniMart.Application.DTOs;
using MiniMart.Application.DTOs.ViewModel;

namespace MiniMart.Infatructure.Abstract
{
    public interface IProductService
    {
        Task<ResponseModel> CreateProduct(ProductViewModel productVM);
        Task<bool> DeleteProduct(int? id);
        Task<string> GenerateCodeAsync();
        Task<ResponseDataTableModel<ProductDto>> GetListProductPagination(RequestDataTableModel request);
        Task<ProductViewModel> GetProductById(int? id);
    }
}