using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniMart.Domain.Entities
{
    public class Product : BaseEntity
    {
        [Required]
        [StringLength(500)]
        public string Name { get; set; }
        [Required]
        [StringLength(500)]
        public string Code { get; set; }
        [StringLength(1000)]
        public string? Description { get; set; }
        [Required]
        public int Available { get; set; }
        public double Price { get; set; }
        public DateTime CreateOn { get; set; }
        public bool IsActive { get; set; }
        public string Image { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }
    }
}
