namespace MiniMart.Application.DTOs.VnPay
{
    public class RequestVnPayModel
    {
        public string OrderId { get; set; }
        public string Fullname { get; set; }
        public string Description { get; set; }
        public double TotalAmount { get; set; }
    }
}
