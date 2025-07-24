using TechFoodClean.Common.DTO.Enums;
using TechFoodClean.Domain.Entities;

namespace TechFoodClean.Application.Presenters
{
    public class PaymentPresenter
    {
        public Guid Id { get; set; }

        public string QrCodeData { get; set; } = null!;

        public string Status { get; set; }

        public static PaymentPresenter Create(Payment payment)
        {
            return new PaymentPresenter()
            {
                Id = payment.Id,
                QrCodeData = payment.QrCodeData,
                Status = payment.Status.ToString()
            };
        }
    }
}
