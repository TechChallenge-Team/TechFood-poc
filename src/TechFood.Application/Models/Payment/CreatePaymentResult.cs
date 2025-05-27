using System;

namespace TechFood.Application.Models.Payment;

public class CreatePaymentResult
{
    public Guid Id { get; set; }

    public string QrCodeData { get; set; } = null!;
}
