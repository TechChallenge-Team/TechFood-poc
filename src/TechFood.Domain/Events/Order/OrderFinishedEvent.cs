using System;
using TechFood.Domain.Shared.Interfaces;

namespace TechFood.Domain.Events.Order;

public record class OrderFinishedEvent(
    Guid Id,
    Guid? CustomerId,
    DateTime FinishedAt) : IDomainEvent;
