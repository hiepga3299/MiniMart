using MiniMart.Application.DTOs;
using MiniMart.Application.DTOs.Chart;
using MiniMart.Application.DTOs.Order;
using MiniMart.Application.DTOs.OrderDetail;
using MiniMart.Application.DTOs.Report;

namespace MiniMart.Infatructure.Abstract
{
    public interface IOrderService
    {
        Task<bool> ComfirmOrder(string id);
        Task<ResponseDataTableModel<object>> GetByPagination(RequestDataTableModel request);
        Task<IEnumerable<ChartOrderByProductDto>> GetChartDataBuProduct();
        Task<IEnumerable<OrderDetailDto>> GetOrderDetail(string orderId);
        Task<ReportOrderDto> GetReportByIdAsync(string id);
        Task<double> GetTotalAmount();
        Task<int> GetTotalOrder();
        Task<bool> SaveAsync(OrderRequestDto productOrder);
    }
}