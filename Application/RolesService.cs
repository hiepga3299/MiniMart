using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiniMart.Infatructure.Abstract;

namespace MiniMart.Application
{
    public class RolesService : IRolesService
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<IEnumerable<SelectListItem>> GetRolesForDropDownList()
        {
            var role = await _roleManager.Roles.ToListAsync();
            return role.Select(x => new SelectListItem
            {
                Value = x.Name,
                Text = x.Name
            });
        }
    }
}
