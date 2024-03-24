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
        private IProductRepository? _productRepository;
        public IProductRepository ProductRepository => _productRepository ??= new ProductRepository(_context, _sqlQueryHandler);

        private ICategoryRepository? _categoryRepository;
        public ICategoryRepository CategoryRepository => _categoryRepository ??= new CategoryRepository(_context);

        public async Task SaveChage()
        {
            await _context.SaveChangesAsync();
        }
    }
}
