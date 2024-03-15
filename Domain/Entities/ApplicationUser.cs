using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MiniMart.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(500)]
        public string? Fullname { get; set; }
        [StringLength(1000)]
        public string? Address { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
