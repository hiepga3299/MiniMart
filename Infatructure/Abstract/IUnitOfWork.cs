
using Microsoft.EntityFrameworkCore;

namespace MiniMart.Infatructure.Abstract
{
	public interface IUnitOfWork
	{
		IProductRepository ProductRepository { get; }
		ICategoryRepository CategoryRepository { get; }
		IUserAddressRepository UserAddressRepository { get; }
		IOrderRepository OrderRepository { get; }
		ICartRepository CartRepository { get; }

		Task BeginTransaction();
		Task CommitTransaction();
		Task RollbackTransaction();
		Task SaveChage();
		DbSet<T> Table<T>() where T : class;
	}
}