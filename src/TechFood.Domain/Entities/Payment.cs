using System;
using TechFood.Domain.Enums;
using TechFood.Domain.Shared.Entities;
using TechFood.Domain.Shared.Exceptions;

namespace TechFood.Domain.Entities;

public class Payment : Entity, IAggregateRoot
{
    private Payment() { }

    public Payment(
        Guid orderId,
        PaymentType type,
        decimal amount)
    {
        OrderId = orderId;
        Type = type;
        Amount = amount;
        CreatedAt = DateTime.Now;
        Status = PaymentStatusType.Pending;
    }

    public Guid OrderId { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime? PaidAt { get; private set; }

    public PaymentType Type { get; private set; }

    public PaymentStatusType Status { get; private set; }

    public decimal Amount { get; private set; }

    public void Pay()
    {
        if (PaidAt.HasValue)
        {
            throw new DomainException(Resources.Exceptions.Payment_AlreadyPaid);
        }

        PaidAt = DateTime.Now;
        Status = PaymentStatusType.Approved;
    }

    public void Refused()
    {
        if (PaidAt.HasValue)
        {
            throw new DomainException(Resources.Exceptions.Payment_AlreadyPaid);
        }

        Status = PaymentStatusType.Refused;
    }
}
