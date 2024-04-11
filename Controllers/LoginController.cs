using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MiniMart.Domain.Entities;
using MiniMart.Infatructure.Abstract;
using MiniMart.Models;

namespace MiniMart.Controllers
{
	public class LoginController : Controller
	{
		private readonly IUserService _userService;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public LoginController(IUserService userService, SignInManager<ApplicationUser> signInManager)
		{
			_userService = userService;
			_signInManager = signInManager;
		}
		public IActionResult Index(string returnUrl)
		{
			LoginPageSizeModel loginModel = new LoginPageSizeModel();
			ViewBag.ReturnUrl = returnUrl;
			return View(loginModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginPageSizeModel loginModel)
		{
			if (!ModelState.IsValid)
			{
				var error = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
				ViewBag.Error = string.Join("<br/>", error);
			}
			var result = await _userService.CheckLogin(loginModel.Username, loginModel.Password, loginModel.RememberMe);
			if (result.Status)
			{
				string returnUrl = loginModel.ReturnUrl;
				if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1
					&& returnUrl.StartsWith("/") && !returnUrl.StartsWith("//")
					&& !returnUrl.StartsWith("/\\"))
				{
					return Redirect(returnUrl);
				}
				return RedirectToAction("Index", "Home");
			}
			TempData["error"] = result.Message;

			return View(loginModel);
		}

		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("", "Home");
		}
	}
}
