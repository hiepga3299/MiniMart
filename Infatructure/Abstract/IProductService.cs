using MiniMart.Application.DTOs;
using MiniMart.Application.DTOs.Products;
using MiniMart.Application.DTOs.ViewModel;

namespace MiniMart.Infatructure.Abstract
{
    public interface IProductService
    {
        Task<ResponseModel> CreateProduct(ProductViewModel productVM);
        Task<bool> DeleteProduct(int? id);
        Task<string> GenerateCodeAsync();
        Task<ProductForSiteModel> GetListProductForSiteAsync(int categoryId, int pageIndex, int pageSize);
        Task<ResponseDataTableModel<ProductDto>> GetListProductPagination(RequestDataTableModel request);
        Task<ProductViewModel> GetProductById(int? id);
    }
}