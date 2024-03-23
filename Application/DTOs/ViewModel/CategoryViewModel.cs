using System.ComponentModel.DataAnnotations;

namespace MiniMart.Application.DTOs.ViewModel
{
    public class CategoryViewModel
    {
        public int? Id { get; set; } = null;
        [Required(ErrorMessage = "Cannot empty")]
        public string Name { get; set; }
    }
}
