namespace MiniMart.Application.DTOs.Products
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int Available { get; set; }
        public double Price { get; set; }
        public string Code { get; set; }
        public DateTime CreateOn { get; set; }
    }
}
