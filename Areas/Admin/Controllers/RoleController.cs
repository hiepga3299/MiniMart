using Microsoft.AspNetCore.Mvc;
using MiniMart.Application.DTOs;
using MiniMart.Infatructure.Abstract;
using MiniMart.Ultility;

namespace MiniMart.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        private readonly IRolesService _rolesManager;

        public RoleController(IRolesService rolesManager)
        {
            _rolesManager = rolesManager;
        }
        [Breadscrum("Danh sách Roles", "Người dùng")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetRolePagination(RequestDataTableModel request)
        {
            var roles = await _rolesManager.GetListRolePagination(request);
            return Json(roles);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveData(RoleDto role)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("error", "Nội dung chưa hợp lệ");
            }
            var result = await _rolesManager.CreateRole(role);
            if (result.Status)
            {
                return RedirectToAction("", "Role");
            }
            ModelState.AddModelError("error", result.Message);
            return Json(role);
        }
    }
}
