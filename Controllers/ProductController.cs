using Microsoft.AspNetCore.Mvc;
using MiniMart.Infatructure.Abstract;

namespace MiniMart.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _product;
        private readonly ICategoryService _category;

        public ProductController(IProductService product, ICategoryService category)
        {
            _product = product;
            _category = category;
        }


        public async Task<IActionResult> Index(int c, int idx = 1, int ps = 8, string kw = null)
        {
            var result = await _product.GetListProductForSiteAsync(c, idx, ps, kw);
            ViewBag.CurrentCategory = await _category.GetById(c);
            ViewBag.CurrentPageIndex = idx;
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductPagination(int category, int pageIndex, string keyword = null)
        {
            int pageSize = 8;
            var result = await _product.GetListProductForSiteAsync(category, pageIndex, pageSize, keyword);
            return Json(result);
        }
        public async Task<IActionResult> Search(string keyword)
        {
            var result = await _product.GetListProductForSiteAsync(0, 1, 8, keyword);
            return View("Search", result);
        }

        [HttpGet]
        public async Task<IActionResult> ProductDetail(int id)
        {
            var product = await _product.GetProductById(id);
            if (product.Id != 0)
            {
                var category = await _category.GetById(product.CategoryId);
                ViewBag.CtName = category.Name.ToString();
            }
            return View(product);
        }
    }
}
