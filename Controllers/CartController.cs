using Microsoft.AspNetCore.Mvc;
using MiniMart.Models;
using MiniMart.Ultility;

namespace MiniMart.Controllers
{
    public class CartController : Controller
    {
        private const string CartSessionName = "CartSession";
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCart(CartModel cart)
        {
            var carts = HttpContext.Session.Get<List<CartModel>>(CartSessionName) ?? new List<CartModel>();
            if (!carts.Any())
            {
                carts.Add(cart);
            }
            else
            {
                var cartExist = carts.FirstOrDefault(x => x.ProductCode == cart.ProductCode);
                if (cartExist is null)
                {
                    carts.Add(cart);
                }
                else
                {
                    cartExist.Quantity += cart.Quantity;
                }
            }
            HttpContext.Session.Set(CartSessionName, carts);

            return Json(carts.Count);
        }
    }
}
