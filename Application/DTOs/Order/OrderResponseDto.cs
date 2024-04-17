using MiniMart.Domain.Entities.Enum;
using MiniMart.Domain.Enum;

namespace MiniMart.Application.DTOs.Order
{
    public class OrderResponseDto
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public DateTime CreateOn { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public StatusProcessing Status { get; set; }
        public string Fullname { get; set; }
        public double TotalPrice { get; set; }
    }
}
