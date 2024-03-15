using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniMart.Domain.Entities
{
    public class CartDetail : BaseEntity
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        [StringLength(1000)]
        public string? Note { get; set; }
        [ForeignKey(nameof(CartId))]
        public Cart Cart { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
    }
}
