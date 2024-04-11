using AutoMapper;
using MiniMart.Application.DTOs.Cart;
using MiniMart.Domain.Entities;
using MiniMart.Infatructure.Abstract;

namespace MiniMart.Application.Services
{
	public class CartService : ICartService
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		public CartService(IMapper mapper, IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}

		public async Task<bool> SaveAsync(CartRequestDto productCart)
		{
			try
			{
				var cart = _mapper.Map<Cart>(productCart);
				await _unitOfWork.CartRepository.Save(cart);
				await _unitOfWork.BeginTransaction();
				await _unitOfWork.SaveChage();
				if (productCart.Products.Any())
				{
					foreach (var product in productCart.Products)
					{
						var cartDetail = new CartDetail
						{
							CartId = cart.Id,
							ProductId = product.Id,
							Quantity = product.Quantity,
							Price = product.Price,

						};
						await _unitOfWork.Table<CartDetail>().AddAsync(cartDetail);
					}
					await _unitOfWork.SaveChage();
				}
				await _unitOfWork.CommitTransaction();
			}
			catch (Exception ex)
			{
				await _unitOfWork.RollbackTransaction();
				return false;
			}
			return true;
		}
	}
}
