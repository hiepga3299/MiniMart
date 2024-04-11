using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Filters;
using MiniMart.Models;

namespace MiniMart.Ultility
{
    public class CommonDataActionFilter : ActionFilterAttribute
    {
        private const string CartSessionName = "CartSession";
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var carts = context.HttpContext.Session.Get<List<CartModel>>(CartSessionName);
            if (carts is not null)
            {
                var controller = context.Controller as Controller;
                controller.ViewData["NumberCart"] = carts.Count();
            }
        }
    }

    public class SiteAreaConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            var areaAttribute = controller.Attributes.OfType<AreaAttribute>().FirstOrDefault();
            if (string.IsNullOrEmpty(areaAttribute?.RouteValue))
            {
                controller.Filters.Add(new CommonDataActionFilter());
            }
        }
    }
}
