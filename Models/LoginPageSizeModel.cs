using System.ComponentModel.DataAnnotations;

namespace MiniMart.Models
{
    public class LoginPageSizeModel
    {
        [Required(ErrorMessage = "Username must be not empty")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Username must be not empty")]
        [MinLength(8, ErrorMessage = "Password must be greater than 8 characters")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }
}
