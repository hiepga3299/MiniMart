using MiniMart.Application.Configuration;
using MiniMart.Application.DTOs.VnPay;
using MiniMart.Infatructure.Abstract;

namespace MiniMart.Application.Services
{
    public class VnPayService : IVnPayService
    {
        private readonly IConfiguration _configuration;

        public VnPayService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string CreatePaymentUrl(HttpContext context, RequestVnPayModel model)
        {
            string vnp_TmnCode = _configuration["VnPay:vnp_TmnCode"];
            string vnp_HashSecret = _configuration["VnPay:vnp_HashSecret"];
            string vnp_Url = _configuration["VnPay:vnp_Url"];
            string vnp_Returnurl = _configuration["VnPay:vnp_Returnurl"];
            string vnp_Version = _configuration["VnPay:vnp_Version"];

            VnPayLibrary vnpay = new VnPayLibrary();

            vnpay.AddRequestData("vnp_Version", _configuration["VnPay:vnp_Version"]);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", _configuration["VnPay:vnp_TmnCode"]);
            vnpay.AddRequestData("vnp_Amount", (model.TotalAmount * 100).ToString());
            vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress(context));
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + model.OrderId);
            vnpay.AddRequestData("vnp_OrderType", "other");
            vnpay.AddRequestData("vnp_ReturnUrl", _configuration["VnPay:vnp_Returnurl"]);
            vnpay.AddRequestData("vnp_TxnRef", $"{model.OrderId}");

            string paymentUrl = vnpay.CreateRequestUrl(_configuration["VnPay:vnp_Url"], _configuration["VnPay:vnp_HashSecret"]);

            return paymentUrl;
        }

        public ResponsePaymentVnPay ResponsePayment(IQueryCollection request)
        {

            VnPayLibrary vnpay = new VnPayLibrary();
            foreach (var (key, value) in request)
            {
                vnpay.AddResponseData(key, value.ToString());
            }
            var vnp_orderId = vnpay.GetResponseData("vnp_TxnRef");
            var vnp_TransactionId = vnpay.GetResponseData("vnp_TransactionNo");
            var vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
            var vnp_SecureHash = request.FirstOrDefault(p => p.Key == "vnp_SecureHash").Value;

            bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, _configuration["VnPay:vnp_HashSecret"]);
            if (!checkSignature)
            {
                return new ResponsePaymentVnPay
                {
                    Success = false
                };
            }
            return new ResponsePaymentVnPay
            {
                OrderId = vnp_orderId,
                TransactionId = vnp_TransactionId,
                ResponseCode = vnp_ResponseCode,
                Token = vnp_SecureHash,
                Success = true
            };
        }
    }
}
