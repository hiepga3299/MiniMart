using Microsoft.EntityFrameworkCore;
using MiniMart.Infatructure.DataAccess;
using System.Linq.Expressions;

namespace MiniMart.Infatructure.Repository
{
    public class RepositoryBase<T> where T : class
    {
        private readonly MiniMartDbContext _context;

        public RepositoryBase(MiniMartDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression = null)
        {
            if (expression != null)
            {
                return await _context.Set<T>().Where(expression).ToListAsync();
            }
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetSingleAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(expression);
        }

        public async Task Create(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async void Update(T entyti)
        {
            //Dùng để đính kèm entity vào DB và theo dõi sự thay đổi và tự động cập nhập trong DB của đối tượng entity 
            _context.Set<T>().Attach(entyti);
            _context.Entry(entyti).State = EntityState.Modified;
        }

        public void Delete(T entyti)
        {
            _context.Set<T>().Attach(entyti);
            _context.Entry(entyti).State = EntityState.Deleted;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
