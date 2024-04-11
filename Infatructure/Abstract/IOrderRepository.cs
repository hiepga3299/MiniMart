using MiniMart.Domain.Entities;

namespace MiniMart.Infatructure.Abstract
{
	public interface IOrderRepository
	{
		Task SaveAsync(Order order);
	}
}