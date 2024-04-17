using Microsoft.AspNetCore.Mvc;
using MiniMart.Application.DTOs;
using MiniMart.Infatructure.Abstract;
using MiniMart.Ultility;

namespace MiniMart.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [Breadscrum("Danh sách Order", "Report")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetByPagination(RequestDataTableModel requestDataTable)
        {
            var data = await _orderService.GetByPagination(requestDataTable);
            return Json(data);
        }
        [Breadscrum("Chi tiết Order", "Report")]
        [HttpGet]
        public async Task<IActionResult> OrderDetail(string orderId)
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderDetail(string orderId)
        {
            var data = await _orderService.GetOrderDetail(orderId);
            return Json(data);
        }
    }
}
