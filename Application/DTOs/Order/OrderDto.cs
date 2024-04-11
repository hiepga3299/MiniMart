using MiniMart.Domain.Enum;

namespace MiniMart.Application.DTOs.Order
{
	public class OrderDto
	{
		public string Id { get; set; }
		public string Code { get; set; }
		public DateTime CreateOn { get; set; }
		public double TotalAmoun { get; set; }
		public PaymentMethod PaymentMethod { get; set; }
		public string? UserId { get; set; }
	}
}
