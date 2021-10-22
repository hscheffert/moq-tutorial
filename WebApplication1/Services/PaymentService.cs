using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IPaymentService
    {
        bool Charge(double total, ICard card);
    }

    //public class PaymentService : IPaymentService
    //{

    //}
}
