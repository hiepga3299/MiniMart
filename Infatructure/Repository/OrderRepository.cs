using Dapper;
using MiniMart.Domain.Entities;
using MiniMart.Infatructure.Abstract;
using MiniMart.Infatructure.DataAccess;

namespace MiniMart.Infatructure.Repository
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        private readonly ISQLQueryHandler _sqlQueryHandler;

        public OrderRepository(MiniMartDbContext context, ISQLQueryHandler sqlQueryHandler) : base(context)
        {
            _sqlQueryHandler = sqlQueryHandler;
        }

        public async Task<IEnumerable<Order>> GetAllOrder()
        {
            var orders = await base.GetAllAsync();
            return orders;
        }

        public async Task<(IEnumerable<T>, int)> GetByPagination<T>(string keyword, int pageIndex, int pageSize)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("keyword", keyword, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            parameters.Add("pageIndex", pageIndex + 1, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            parameters.Add("pageSize", pageSize, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            parameters.Add("totalRecord", 0, System.Data.DbType.Int32, System.Data.ParameterDirection.Output);

            var result = await _sqlQueryHandler.ExecuteStoreProdecureReturnList<T>("GetAllOrderByPagination", parameters);
            var totalRecord = parameters.Get<int>("totalRecord");

            return (result, totalRecord);
        }

        public async Task<IEnumerable<T>> GetOrderDetail<T>(string orderId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("orderId", orderId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            var result = await _sqlQueryHandler.ExecuteStoreProdecureReturnList<T>("GetOrderDetail", parameters);
            return result;
        }

        public async Task<IEnumerable<T>> GetChartDataByProduct<T>()
        {
            var result = await _sqlQueryHandler.ExecuteStoreProdecureReturnList<T>("GetChartDataOrderByProduct");
            return result;
        }

        public async Task<Order> GetOrderById(string id)
        {
            return await GetSingleAsync(x => x.Id == id);
        }
        public async Task Update(Order order)
        {
            await base.Update(order);
        }


        public async Task SaveAsync(Order order)
        {
            await base.Create(order);
        }
    }
}
