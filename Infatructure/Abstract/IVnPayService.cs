using MiniMart.Application.DTOs.VnPay;

namespace MiniMart.Infatructure.Abstract
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(HttpContext context, RequestVnPayModel model);
        ResponsePaymentVnPay ResponsePayment(IQueryCollection request);
    }
}