using MiniMart.Application.DTOs;

namespace MiniMart.Infatructure.Abstract
{
    public interface ICategoryService
    {
        Task<ResponseDataTableModel<CategoryDto>> GetListCategory(RequestDataTableModel requestData);
    }
}