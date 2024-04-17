using Microsoft.AspNetCore.Mvc;
using MiniMart.Application.DTOs.Report;
using MiniMart.Infatructure.Abstract;
using MiniMart.Infatructure.Services;
using MiniMart.Ultility;

namespace MiniMart.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReportController : Controller
    {
        private readonly IPDFServices _pdfServices;
        private readonly IOrderService _orderService;

        public ReportController(IPDFServices pdfServices, IOrderService orderService)
        {
            _pdfServices = pdfServices;
            _orderService = orderService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ExportPdfOder(string id)
        {
            var order = await _orderService.GetReportByIdAsync(id);
            var html = await this.RenderViewAsync<ReportOrderDto>(RouteData, "_TeamplateReport", order, true);
            var result = _pdfServices.GeneratePDF(html);
            return File(result, "application/pdf", $"{DateTime.Now.Ticks}.pdf");
        }
    }
}
