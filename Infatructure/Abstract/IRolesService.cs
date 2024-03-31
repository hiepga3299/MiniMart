using Microsoft.AspNetCore.Mvc.Rendering;
using MiniMart.Application.DTOs;
using MiniMart.Application.DTOs.Accounts;

namespace MiniMart.Infatructure.Abstract
{
    public interface IRolesService
    {
        Task<ResponseModel> CreateRole(RoleDto roleDto);
        Task<ResponseDataTableModel<RoleDto>> GetListRolePagination(RequestDataTableModel request);
        Task<IEnumerable<SelectListItem>> GetRolesForDropDownList();
    }
}