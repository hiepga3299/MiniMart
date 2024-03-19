using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniMart.Ultility;

namespace MiniMart.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    [Breadscrum("Dashboard")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
