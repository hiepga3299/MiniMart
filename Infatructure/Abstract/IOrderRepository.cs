using MiniMart.Domain.Entities;

namespace MiniMart.Infatructure.Abstract
{
    public interface IOrderRepository
    {
        Task<(IEnumerable<T>, int)> GetByPagination<T>(string keyword, int pageIndex, int pageSize);
        Task<IEnumerable<T>> GetChartDataByProduct<T>();
        Task<Order> GetOrderById(string id);
        Task<IEnumerable<T>> GetOrderDetail<T>(string orderId);
        Task SaveAsync(Order order);
        Task Update(Order order);
    }
}