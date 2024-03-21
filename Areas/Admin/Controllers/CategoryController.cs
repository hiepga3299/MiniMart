using Microsoft.AspNetCore.Mvc;
using MiniMart.Application.DTOs;
using MiniMart.Infatructure.Abstract;
using MiniMart.Ultility;

namespace MiniMart.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [Area("Admin")]
        [Breadscrum("Danh sách danh mục", "Cửa hàng")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetCategoryPagination(RequestDataTableModel requestData)
        {
            var result = await _categoryService.GetListCategory(requestData);
            return Json(result);
        }
    }
}
