using MiniMart.Application.DTOs;
using MiniMart.Application.DTOs.Order;
using MiniMart.Application.DTOs.OrderDetail;
using MiniMart.Application.DTOs.Report;

namespace MiniMart.Infatructure.Abstract
{
    public interface IOrderService
    {
        Task<ResponseDataTableModel<object>> GetByPagination(RequestDataTableModel request);
        Task<IEnumerable<OrderDetailDto>> GetOrderDetail(string orderId);
        Task<ReportOrderDto> GetReportByIdAsync(string id);
        Task<bool> SaveAsync(OrderRequestDto productOrder);
    }
}