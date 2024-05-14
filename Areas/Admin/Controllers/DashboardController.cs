using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniMart.Infatructure.Abstract;
using MiniMart.Ultility;

namespace MiniMart.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    [Breadscrum("Dashboard")]
    public class DashboardController : Controller
    {
        private readonly IOrderService _order;

        public DashboardController(IOrderService order)
        {
            _order = order;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetChartDataByProduct()
        {
            return Json(await _order.GetChartDataBuProduct());
        }
    }
}
