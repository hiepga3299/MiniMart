using MiniMart.Application.DTOs.Order;

namespace MiniMart.Infatructure.Abstract
{
	public interface IOrderService
	{
		Task<bool> SaveAsync(OrderRequestDto productOrder);
	}
}