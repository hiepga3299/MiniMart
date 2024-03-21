namespace MiniMart.Application.DTOs
{
    public class AccountDto
    {
        public string? Id { get; set; }
        public string Role { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool isActive { get; set; }
        public IFormFile Avarta { get; set; }
    }
}
