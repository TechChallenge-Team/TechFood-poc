using System;
using TechFood.Domain.Enums;
using TechFood.Domain.Shared.Interfaces;

namespace TechFood.Domain.Events.Payment;

public record class PaymentRefusedEvent(
    Guid Id,
    Guid OrderId,
    DateTime RefusedAt,
    PaymentType Type,
    decimal Amount) : IDomainEvent;
