using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniMart.Domain.Entities
{
    public class Cart : BaseEntity
    {
        [StringLength(100)]
        public string? Code { get; set; }
        public DateTime CreateOn { get; set; }
        [StringLength(1000)]
        public string? Note { get; set; }
        [Required]
        public int Status { get; set; }
        public bool IsActive { get; set; }
        public string? UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }
    }
}
