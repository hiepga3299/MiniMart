using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniMart.Domain.Entities
{
    public class Address : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Phone { get; set; }
        [Required]
        [StringLength(100)]
        public string AddressName { get; set; }
        [Required]
        [StringLength(100)]
        public string Fullname { get; set; }
        [StringLength(100)]
        public string? Email { get; set; }
        public bool IsActive { get; set; }
        public string? UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }
    }
}
