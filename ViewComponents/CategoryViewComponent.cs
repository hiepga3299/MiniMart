using Microsoft.AspNetCore.Mvc;
using MiniMart.Infatructure.Abstract;

namespace MiniMart.ViewComponents
{
    public class CategoryViewComponent : ViewComponent
    {
        public ICategoryService _category { get; }
        public CategoryViewComponent(ICategoryService category)
        {
            _category = category;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var category = await _category.GetListCategoryForSite();
            return View("RenderHeader", category);
        }
    }
}
