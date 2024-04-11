using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MiniMart.Infatructure.Abstract;
using MiniMart.Infatructure.DataAccess;

namespace MiniMart.Infatructure.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly MiniMartDbContext _context;
		private readonly ISQLQueryHandler _sqlQueryHandler;

		public UnitOfWork(MiniMartDbContext context, ISQLQueryHandler sqlQueryHandler)
		{
			_context = context;
			_sqlQueryHandler = sqlQueryHandler;
		}
		public DbSet<T> Table<T>() where T : class => _context.Set<T>();
		private IProductRepository? _productRepository;
		public IProductRepository ProductRepository => _productRepository ??= new ProductRepository(_context, _sqlQueryHandler);

		private ICategoryRepository? _categoryRepository;
		public ICategoryRepository CategoryRepository => _categoryRepository ??= new CategoryRepository(_context);

		private ICartRepository? _cartRepository;
		public ICartRepository CartRepository => _cartRepository ??= new CartRepository(_context);

		private IOrderRepository? _orderRepository;
		public IOrderRepository OrderRepository => _orderRepository ??= new OrderRepository(_context);

		private IUserAddressRepository? _userAddressRepository;
		public IUserAddressRepository UserAddressRepository => _userAddressRepository ??= new UserAddressRepository(_context);

		IDbContextTransaction _dbContextTransaction;
		public async Task BeginTransaction()
		{
			_dbContextTransaction = await _context.Database.BeginTransactionAsync();
		}
		public async Task CommitTransaction()
		{
			await _dbContextTransaction?.CommitAsync();
		}
		public async Task RollbackTransaction()
		{
			await _dbContextTransaction?.RollbackAsync();
		}

		public async Task SaveChage()
		{
			await _context.SaveChangesAsync();
		}
	}
}
