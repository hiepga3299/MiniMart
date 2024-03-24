namespace MiniMart.Application.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public int Available { get; set; }
        public double Price { get; set; }
        public DateTime CreateOn { get; set; }
    }
}
