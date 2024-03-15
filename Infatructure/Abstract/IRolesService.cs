using Microsoft.AspNetCore.Mvc.Rendering;

namespace MiniMart.Infatructure.Abstract
{
    public interface IRolesService
    {
        Task<IEnumerable<SelectListItem>> GetRolesForDropDownList();
    }
}