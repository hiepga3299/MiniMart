using Microsoft.AspNetCore.Mvc.Rendering;
using MiniMart.Application.DTOs;
using MiniMart.Application.DTOs.Categories;
using MiniMart.Application.DTOs.ViewModel;

namespace MiniMart.Infatructure.Abstract
{
    public interface ICategoryService
    {
        Task CreateCategory(CategoryViewModel categoryViewModel);
        Task<bool> DeleteProduct(int id);
        Task<CategoryViewModel> GetById(int id);
        Task<IEnumerable<SelectListItem>> GetCategoryForDropDownListAsync();
        Task<ResponseDataTableModel<CategoryDto>> GetListCategory(RequestDataTableModel requestData);
        Task<IEnumerable<CategoryDto>> GetListCategoryForSite();
    }
}