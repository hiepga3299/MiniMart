using AutoMapper;
using MiniMart.Application.DTOs.Order;
using MiniMart.Domain.Entities;
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
