using System.ComponentModel.DataAnnotations;

namespace MiniMart.Application.DTOs.ViewModel
{
    public class CategoryViewModel
    {
        public int? Id { get; set; } = null;
        [Required(ErrorMessage = "Không được để trống")]
        public string Name { get; set; }
    }
}
