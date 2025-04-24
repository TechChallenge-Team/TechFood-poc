using TechFood.Domain.Enums;

namespace TechFood.Application.Models.Order
{
    public class CreatePaymentRequest
    {
        public PaymentType Type { get; set; }
    }
}
