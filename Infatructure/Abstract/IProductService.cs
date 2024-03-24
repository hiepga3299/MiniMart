using MiniMart.Application.DTOs;
using MiniMart.Application.DTOs.ViewModel;

namespace MiniMart.Infatructure.Abstract
{
    public interface IProductService
    {
        Task<ResponseDataTableModel<ProductDto>> GetListProductPagination(RequestDataTableModel request);
        Task<ProductViewModel> GetProductById(int? id);
    }
}