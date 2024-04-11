using MiniMart.Application.DTOs.Products;
using MiniMart.Domain.Entities.Enum;
using MiniMart.Domain.Enum;

namespace MiniMart.Application.DTOs.Order
{
	public class OrderRequestDto
	{
		public string Id { get; set; }
		public string Code { get; set; }
		public DateTime CreateOn { get; set; }
		public double TotalAmoun { get; set; }
		public PaymentMethod PaymentMethod { get; set; }
		public StatusProcessing Status { get; set; }
		public string? UserId { get; set; }
		public List<ProductCartDto> Products { get; set; }
	}
}
