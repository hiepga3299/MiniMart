using MiniMart.Application.DTOs.Cart;

namespace MiniMart.Infatructure.Abstract
{
	public interface ICartService
	{
		Task<bool> SaveAsync(CartRequestDto productCart);
	}
}