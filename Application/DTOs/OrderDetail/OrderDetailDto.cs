namespace MiniMart.Application.DTOs.OrderDetail
{
    public class OrderDetailDto
    {
        public string FullName { get; set; }
        public string AddressName { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public string Code { get; set; }
        public int Quantity { get; set; }
    }
}
