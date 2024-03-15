using System.ComponentModel.DataAnnotations;

namespace MiniMart.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username must be not empty")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Username must be not empty")]
        [MinLength(8, ErrorMessage = "Password must be greater than 8 characters")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
