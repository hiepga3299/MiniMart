﻿using Microsoft.AspNetCore.Mvc;
using MiniMart.Infatructure.Abstract;
using MiniMart.Models;
using MiniMart.Ultility;

namespace MiniMart.Controllers
{
    public class CartController : Controller
    {
        public IProductService _product { get; }
        public CartController(IProductService product)
        {
            _product = product;
        }
        private const string CartSessionName = "CartSession";


        public async Task<IActionResult> Index()
        {
            var carts = HttpContext.Session.Get<List<CartModel>>(CartSessionName) ?? new List<CartModel>();
            if (carts is not null)
            {
                var codes = carts.Select(x => x.ProductCode).ToArray();
                var products = await _product.GetProductByCodeAsync(codes);
                products = products.Select(product =>
                {
                    var item = carts.FirstOrDefault(x => x.ProductCode == product.Code);
                    if (item is not null)
                    {
                        product.Quantity = item.Quantity;
                    }
                    return product;
                });
                return View(products);
            }
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
