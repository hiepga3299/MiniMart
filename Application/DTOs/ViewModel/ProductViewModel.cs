namespace MiniMart.Application.DTOs.ViewModel
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int Available { get; set; }
        public double Price { get; set; }
        public DateTime CreateOn { get; set; }
        public bool IsActive { get; set; }
    }
}
