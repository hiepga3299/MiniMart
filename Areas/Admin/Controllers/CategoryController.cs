using Microsoft.AspNetCore.Mvc;
using MiniMart.Application.DTOs;
using MiniMart.Application.DTOs.ViewModel;
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
        [Breadscrum("Danh sách danh mục", "Cửa hàng")]
        public IActionResult Index()
        {
            var categoryDto = new CategoryDto();
            return View(categoryDto);
        }

        [HttpPost]
        public async Task<IActionResult> GetCategoryPagination(RequestDataTableModel requestData)
        {
            var result = await _categoryService.GetListCategory(requestData);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            return Json(await _categoryService.GetById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveData(CategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.CreateCategory(categoryViewModel);
            }
            return Json(categoryViewModel);
        }
    }
}
