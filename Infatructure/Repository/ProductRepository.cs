using Dapper;
using MiniMart.Domain.Entities;
using MiniMart.Infatructure.Abstract;
using MiniMart.Infatructure.DataAccess;

namespace MiniMart.Infatructure.Repository
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        private readonly MiniMartDbContext _context;
        private readonly ISQLQueryHandler _sqlQueryHandler;

        public ProductRepository(MiniMartDbContext context, ISQLQueryHandler sqlQueryHandler) : base(context)
        {
            _context = context;
            _sqlQueryHandler = sqlQueryHandler;
        }

        public async Task<Product> GetSingleProduct(int? id)
        {

            return await GetSingleAsync(x => x.Id == id);
        }

        public async Task<Product> GetProductByCode(string? code)
        {
            return await GetSingleAsync(x => x.Code == code);
        }

        public async Task<IEnumerable<T>> GetCategoryByProductId<T>(int? id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("ProductId", id, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);

            var result = await _sqlQueryHandler.ExecuteStoreProdecureReturnList<T>("GetCategoryByProductID", parameters);
            return result;
        }

        public async Task<(IEnumerable<T>, int)> GetAllProductPagination<T>(string keyword, int pageIndex, int pageSize)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("keyword", keyword, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            parameters.Add("pageIndex", pageIndex + 1, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            parameters.Add("pageSize", pageSize, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            parameters.Add("totalRecord", 0, System.Data.DbType.Int32, System.Data.ParameterDirection.Output);

            var result = await _sqlQueryHandler.ExecuteStoreProdecureReturnList<T>("GetAllProductPagination", parameters);
            var totalRecords = parameters.Get<int>("totalRecord");

            return (result, totalRecords);
        }

        public async Task<bool?> SaveProduct(Product product)
        {
            if (product.Id == 0)
            {
                await base.Create(product);
            }
            else
            {
                base.Update(product);
            }
            return true;
        }

        public bool? DeleteProduct(Product product)
        {
            if (product.Id != 0)
            {
                base.Delete(product);
            }
            return true;
        }

    }
}
