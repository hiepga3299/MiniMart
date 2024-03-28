using Microsoft.AspNetCore.Mvc;
using MiniMart.Application.DTOs;
using MiniMart.Application.DTOs.ViewModel;
using MiniMart.Infatructure.Abstract;
using MiniMart.Ultility;

namespace MiniMart.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        [Breadscrum("Danh mục sản phẩm", "Cửa hàng")]
        public IActionResult Index()
        {
            var product = new ProductDto();
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> GetProductPagination(RequestDataTableModel request)
        {
            var result = await _productService.GetListProductPagination(request);
            return Json(result);
        }

        [HttpGet]
        [Breadscrum("Thêm sản phẩm", "Cửa hàng")]
        public async Task<IActionResult> SaveData(int id)
        {
            var productVM = new ProductViewModel();
            ViewBag.Category = await _categoryService.GetCategoryForDropDownListAsync();

            string code = await _productService.GenerateCodeAsync();
            productVM.Code = code;
            if (id != 0)
            {
                productVM = await _productService.GetProductById(id);
            }
            return View(productVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveDataAsync(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.CreateProduct(product);
                if (result.Status)
                {
                    return RedirectToAction("", "Product");
                }
                ModelState.AddModelError("error", result.Message);
            }
            else
            {
                ModelState.AddModelError("error", "Dữ liệu nhập sai");
            }
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productService.DeleteProduct(id);
            return Json(1);
        }
    }
}
