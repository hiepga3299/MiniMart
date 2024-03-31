using Microsoft.AspNetCore.Mvc;
using MiniMart.Infatructure.Abstract;

namespace MiniMart.Controllers
{
    public class ProductController : Controller
    {
        public IProductService _product { get; }
        public ProductController(IProductService product)
        {
            _product = product;
        }


        public async Task<IActionResult> Index(int c, int idx = 1, int ps = 8)
        {
            var result = await _product.GetListProductForSiteAsync(c, idx, ps);
            ViewBag.CurrentCategory = c;
            ViewBag.CurrentPageIndex = idx;
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductPagination(int category, int pageIndex)
        {
            int pageSize = 8;
            var result = await _product.GetListProductForSiteAsync(category, pageIndex, pageSize);
            return Json(result);
        }

        [HttpGet]
        public IActionResult ProductDetail(int id)
        {
            return View();
        }
    }
}
