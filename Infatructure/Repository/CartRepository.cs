using MiniMart.Domain.Entities;
using MiniMart.Infatructure.Abstract;
using MiniMart.Infatructure.DataAccess;

namespace MiniMart.Infatructure.Repository
{
	public class CartRepository : RepositoryBase<Cart>, ICartRepository
	{
		public CartRepository(MiniMartDbContext context) : base(context)
		{
		}

		public async Task Save(Cart cart)
		{
			if (cart.Id == 0)
			{
				await base.Create(cart);
			}
			else
			{
				await base.Update(cart);
			}
		}
	}
}
