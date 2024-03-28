using Microsoft.AspNetCore.Mvc.Rendering;
using MiniMart.Application.DTOs;

namespace MiniMart.Infatructure.Abstract
{
    public interface IRolesService
    {
        Task<ResponseModel> CreateRole(RoleDto roleDto);
        Task<ResponseDataTableModel<RoleDto>> GetListRolePagination(RequestDataTableModel request);
        Task<IEnumerable<SelectListItem>> GetRolesForDropDownList();
    }
}