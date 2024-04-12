using Microsoft.AspNetCore.Mvc;
using MiniMart.Application.DTOs;
using MiniMart.Application.DTOs.Cart;
using MiniMart.Application.DTOs.Order;
using MiniMart.Application.DTOs.Products;
using MiniMart.Domain.Common;
using MiniMart.Domain.Entities.Enum;
using MiniMart.Domain.Enum;
using MiniMart.Infatructure.Abstract;
using MiniMart.Models;
using MiniMart.Ultility;
using System.Security.Claims;

namespace MiniMart.Controllers
{
	public class CheckoutController : Controller
	{
		private readonly IProductService _product;
		private readonly ICartService _cartService;
		private readonly IOrderService _orderService;
		private readonly IUserAddressService _userAddressService;

		public CheckoutController(IProductService product, ICartService cartService, IOrderService orderService, IUserAddressService userAddressService)
		{
			_product = product;
			_cartService = cartService;
			_orderService = orderService;
			_userAddressService = userAddressService;
		}
		public async Task<IActionResult> Index(string returnUrl)
		{
			if (!HttpContext.User.Identity.IsAuthenticated)
			{
				return RedirectToAction("", "Login", new { ReturnUrl = returnUrl });
			}
			UserAddressDto address = new UserAddressDto();
			var carts = GetSession();
			ViewBag.Products = await GetCartFromSessionAsync();
			return View(address);
		}

		private List<CartModel>? GetSession()
		{
			return HttpContext.Session.Get<List<CartModel>>(CommonConstant.CartSessionName);
		}
		[HttpPost]
		public async Task<IActionResult> CompleteCart(UserAddressDto userAddressDto)
		{

			string codeOrder = $"ORDER_{DateTime.Now.ToString("ddMMyyyyhhmmss")}";

			if (ModelState.IsValid)
			{
				string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
				var products = await GetCartFromSessionAsync();
				userAddressDto.UserId = userId;
				await _userAddressService.SaveAsync(userAddressDto);

				var cart = new CartRequestDto
				{
					CreateOn = DateTime.Now,
					Code = $"CART_{DateTime.Now.ToString("ddMMyyyyhhmmss")}",
					Status = StatusProcessing.New,
					UserId = userId,
					Products = products.ToList(),
				};
				await _cartService.SaveAsync(cart);

				var order = new OrderRequestDto
				{
					Products = products.ToList(),
					CreateOn = DateTime.Now,
					Code = codeOrder,
					PaymentMethod = userAddressDto.PaymentMethod,
					Status = StatusProcessing.New,
					TotalAmoun = 0,
					UserId = userId,
					Id = userAddressDto.PaymentMethod == PaymentMethod.Paypal ? userAddressDto.UserId : Guid.NewGuid().ToString()

				};
				await _orderService.SaveAsync(order);
			}
			return View();
		}

		private async Task<IEnumerable<ProductCartDto>> GetCartFromSessionAsync()
		{
			List<ProductCartDto> productCartDtos = new List<ProductCartDto>();
			var carts = GetSession();
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
				productCartDtos = products.ToList();
			}
			return productCartDtos;
		}
	}
}
