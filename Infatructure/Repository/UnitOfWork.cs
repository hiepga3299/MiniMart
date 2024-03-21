using MiniMart.Infatructure.Abstract;
using MiniMart.Infatructure.DataAccess;

namespace MiniMart.Infatructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MiniMartDbContext _context;
        public UnitOfWork(MiniMartDbContext context)
        {
            _context = context;
        }
        private IProductRepository? _productRepository;
        public IProductRepository ProductRepository => _productRepository ??= new ProductRepository(_context);

        private ICategoryRepository? _categoryRepository;
        public ICategoryRepository CategoryRepository => _categoryRepository ??= new CategoryRepository(_context);

        public async Task SaveChage()
        {
            await _context.SaveChangesAsync();
        }
    }
}
