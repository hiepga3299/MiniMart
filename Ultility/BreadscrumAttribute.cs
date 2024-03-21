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
                var path = string.IsNullOrEmpty(_mastername) ? $"{_title}" : $"{_mastername}/{_title}";
                controller.ViewData["BreadScrum"] = new BreadScrumModel
                {
                    Title = _title,
                    Path = path
                };
            }
        }
    }
}
