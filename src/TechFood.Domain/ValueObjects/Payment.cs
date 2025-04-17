using System;
using TechFood.Domain.Enums;
using TechFood.Domain.Shared.ValueObjects;

namespace TechFood.Domain.ValueObjects;

public class Payment : ValueObject
{
    public DateTime Date { get; private set; }

    public PaymentStatusType Status { get; private set; }

    public PaymentType Type { get; private set; }
}
