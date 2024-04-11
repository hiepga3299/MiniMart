using MiniMart.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace MiniMart.Domain.Entities
{
    public class Order
    {
        [Required]
        [StringLength(50)]
        public string Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Code { get; set; }
        public DateTime CreateOn { get; set; }
        public double TotalAmoun { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string? UserId { get; set; }
    }
}
