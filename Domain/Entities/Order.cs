using MiniMart.Domain.Entities.Enum;
using MiniMart.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public int AddressId { get; set; }
        [ForeignKey(nameof(AddressId))]
        public Address Address { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public StatusProcessing Status { get; set; }
        [Required]
        [StringLength(50)]
        public string UserId { get; set; }
        public ICollection<OrderDetail> Details { get; set; }
    }
}
