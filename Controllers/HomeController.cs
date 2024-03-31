using Microsoft.AspNetCore.Mvc;
using MiniMart.Infatructure.Abstract;

namespace MiniMart.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _product;

        public HomeController(IProductService product)
        {
            _product = product;
        }

        public async Task<IActionResult> Index(int c = 0, int idx = 1, int ps = 8)
        {
            var products = await _product.GetListProductForSiteAsync(c, idx, ps);
            ViewBag.CurrentCategory = c;
            ViewBag.CurrentPageIndex = idx;
            return View(products);
        }
    }
}