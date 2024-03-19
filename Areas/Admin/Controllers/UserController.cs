using Microsoft.AspNetCore.Mvc;
using MiniMart.Application.DTOs;
using MiniMart.Infatructure.Abstract;
using MiniMart.Ultility;

namespace MiniMart.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRolesService _rolesService;

        public UserController(IUserService userService, IRolesService rolesService)
        {
            _userService = userService;
            _rolesService = rolesService;
        }
        [Breadscrum("Danh sách tài khoản", "Account")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetAccountPagination(RequestModel requestModel)

        {
            var user = await _userService.GetListUser(requestModel);
            return Json(user);
        }
        [HttpGet]
        [Breadscrum("Thêm Tài khoản", "Add Account")]
        public async Task<IActionResult> SaveData(string? id)
        {
            var accountDto = !string.IsNullOrEmpty(id) ? await _userService.GetUserById(id) : new AccountDto();
            ViewBag.Roles = await _rolesService.GetRolesForDropDownList();
            return View(accountDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveData(AccountDto account)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = await _rolesService.GetRolesForDropDownList();
                ModelState.AddModelError("error", "Nội dung chưa hợp lệ");
            }
            var result = await _userService.SaveAccount(account);
            if (result.Status)
            {
                return RedirectToAction("", "User");
            }
            ModelState.AddModelError("error", result.Message);
            ViewBag.Roles = await _rolesService.GetRolesForDropDownList();
            return View(account);
        }
    }
}
