using MiniMart.Domain.Entities;

namespace MiniMart.Infatructure.Abstract
{
	public interface ICartRepository
	{
		Task Save(Cart cart);
	}
}