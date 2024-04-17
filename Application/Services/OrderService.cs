using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MiniMart.Application.DTOs;
using MiniMart.Application.DTOs.Order;
using MiniMart.Application.DTOs.OrderDetail;
using MiniMart.Application.DTOs.Report;
using MiniMart.Domain.Entities;
using MiniMart.Domain.Entities.Enum;
using MiniMart.Domain.Enum;
using MiniMart.Infatructure.Abstract;

namespace MiniMart.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseDataTableModel<object>> GetByPagination(RequestDataTableModel request)
        {
            var (order, totalRecord) = await _unitOfWork.OrderRepository.GetByPagination<OrderResponseDto>(request.Keyword, request.SkipIndex, request.PageSize);

            return new ResponseDataTableModel<object>
            {
                Draw = request.Draw,
                RecordsTotal = totalRecord,
                RecordsFilltered = totalRecord,
                Data = order.Select(x => new
                {
                    Id = x.Id,
                    Code = x.Code,
                    CreateOn = x.CreateOn,
                    Fullname = x.Fullname,
                    TotalPrice = x.TotalPrice,
                    Status = Enum.GetName(typeof(StatusProcessing), x.Status),
                    PaymentMethod = Enum.GetName(typeof(PaymentMethod), x.PaymentMethod),
                }).ToList()
            };
        }

        public async Task<IEnumerable<OrderDetailDto>> GetOrderDetail(string orderId)
        {
            var result = await _unitOfWork.OrderRepository.GetOrderDetail<OrderDetailDto>(orderId);
            return result;
        }

        public async Task<ReportOrderDto> GetReportByIdAsync(string id)
        {
            var order = await _unitOfWork.Table<Order>().Where(x => x.Id == id).Include(x => x.Address).Include(x => x.Details).SingleAsync();
            var address = _mapper.Map<OrderAddressDto>(order.Address);
            var detail = order.Details.Join(_unitOfWork.Table<Product>(),
                                                           x => x.ProductId,
                                                           y => y.Id,
                                                           (detail, product) => new DetailOrderDto
                                                           {
                                                               Price = detail.UnitPrice,
                                                               Quantity = detail.Quantity,
                                                               ProductName = product.Name,
                                                           }).ToList();

            return new ReportOrderDto
            {
                Code = order.Code,
                CreateOn = order.CreateOn,
                Address = address,
                Detail = detail
            };
        }

        public async Task<bool> SaveAsync(OrderRequestDto orderDto)
        {
            try
            {
                var order = _mapper.Map<Order>(orderDto);
                await _unitOfWork.BeginTransaction();
                await _unitOfWork.OrderRepository.SaveAsync(order);
                await _unitOfWork.SaveChage();
                if (orderDto.Products.Any())
                {
                    foreach (var product in orderDto.Products)
                    {
                        var orderDetail = new OrderDetail
                        {
                            OrderId = order.Id,
                            ProductId = product.Id,
                            Quantity = product.Quantity,
                            UnitPrice = product.Price
                        };
                        await _unitOfWork.Table<OrderDetail>().AddAsync(orderDetail);
                    }
                    await _unitOfWork.SaveChage();
                }
                await _unitOfWork.CommitTransaction();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransaction();
                return false;
            }
            return true;
        }
    }
}
