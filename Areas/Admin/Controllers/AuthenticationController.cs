using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MiniMart.Areas.Admin.Models;
using MiniMart.Domain.Entities;
using MiniMart.Infatructure.Abstract;

namespace MiniMart.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthenticationController : Controller
    {
        private readonly IUserService _userService;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthenticationController(IUserService userService, SignInManager<ApplicationUser> signInManager)
        {
            _userService = userService;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Login()
        {
            LoginModel loginModel = new LoginModel();
            return View(loginModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                var error = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                ViewBag.Error = string.Join("<br/>", error);
            }
            var result = await _userService.CheckLogin(loginModel.Username, loginModel.Password, loginModel.RememberMe);
            if (result.Status)
            {
                return RedirectToAction("Index", "Home");
            }
            TempData["error"] = result.Message;

            return View(loginModel);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return View("Login");
        }
    }
}
