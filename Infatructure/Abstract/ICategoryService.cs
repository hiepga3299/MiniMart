using Microsoft.AspNetCore.Mvc.Rendering;
using MiniMart.Application.DTOs;
using MiniMart.Application.DTOs.ViewModel;

namespace MiniMart.Infatructure.Abstract
{
    public interface ICategoryService
    {
        Task CreateCategory(CategoryViewModel categoryViewModel);
        Task<CategoryViewModel> GetById(int id);
        Task<IEnumerable<SelectListItem>> GetCategoryForDropDownListAsync();
        Task<ResponseDataTableModel<CategoryDto>> GetListCategory(RequestDataTableModel requestData);
    }
}