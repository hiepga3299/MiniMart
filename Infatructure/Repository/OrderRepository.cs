using MiniMart.Domain.Entities;
using MiniMart.Infatructure.Abstract;
using MiniMart.Infatructure.DataAccess;

namespace MiniMart.Infatructure.Repository
{
	public class OrderRepository : RepositoryBase<Order>, IOrderRepository
	{
		public OrderRepository(MiniMartDbContext context) : base(context)
		{
		}

		public async Task SaveAsync(Order order)
		{
			await base.Create(order);
		}
	}
}
