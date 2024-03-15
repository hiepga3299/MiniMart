using System.ComponentModel.DataAnnotations;

namespace MiniMart.Domain.Entities
{
    public class Category : BaseEntity
    {
        [Required]
        [StringLength(1000)]
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
    }
}
