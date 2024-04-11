using MiniMart.Application.DTOs.Products;
using MiniMart.Domain.Entities.Enum;

namespace MiniMart.Application.DTOs.Cart
{
	public class CartRequestDto
	{
		public string? Code { get; set; }
		public DateTime CreateOn { get; set; }
		public string? Note { get; set; }
		public StatusProcessing Status { get; set; }
		public string? UserId { get; set; }
		public List<ProductCartDto> Products { get; set; }
	}
}
