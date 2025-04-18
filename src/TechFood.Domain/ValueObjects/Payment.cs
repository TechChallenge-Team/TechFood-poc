using System;
using TechFood.Domain.Enums;
using TechFood.Domain.Shared.ValueObjects;

namespace TechFood.Domain.ValueObjects;

public class Payment : ValueObject
{
    public Payment(PaymentType type, decimal amount)
    {
        Type = type;
        Amount = amount;
        PaidAt = DateTime.Now;
    }

    public DateTime PaidAt { get; private set; }

    public PaymentType Type { get; private set; }

    public decimal Amount { get; private set; }
}
