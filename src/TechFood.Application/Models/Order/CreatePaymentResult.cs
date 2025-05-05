using System;

namespace TechFood.Application.Models.Order
{
    public class CreatePaymentResult
    {
        public Guid Id { get; set; }

        public string QrCodeData { get; set; } = null!;
    }
}
