using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MiniMart.Models;

namespace MiniMart.Ultility
{
    public class BreadscrumAttribute : ActionFilterAttribute, IActionFilter
    {
        private readonly string _title;
        private readonly string _mastername;
        public BreadscrumAttribute(string title, string mastername = "")
        {
            _title = title;
            _mastername = mastername;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.Controller is Controller controller)
            {
                var controllerName = controller.GetType().Name.Replace("Controller", "");
                var path = string.IsNullOrEmpty(_mastername) ? $"{controllerName}" : $"{_mastername}/{controllerName}/{_title}";
                controller.ViewData["BreadScrum"] = new BreadScrumModel
                {
                    Title = _title,
                    Path = path
                };
            }
        }
    }
}
