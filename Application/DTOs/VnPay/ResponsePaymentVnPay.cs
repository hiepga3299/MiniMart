namespace MiniMart.Application.DTOs.VnPay
{
    public class ResponsePaymentVnPay
    {
        public string OrderId { get; set; }
        public string TransactionId { get; set; }
        public string ResponseCode { get; set; }
        public string Token { get; set; }
        public bool Success { get; set; }
    }
}
