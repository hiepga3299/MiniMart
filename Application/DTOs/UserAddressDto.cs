using MiniMart.Domain.Enum;

namespace MiniMart.Application.DTOs
{
    public class UserAddressDto
    {
        public int Id { get; set; } = 0;
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public double TotalPrice { get; set; }
        public string? OrderId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string? UserId { get; set; }
    }
}
