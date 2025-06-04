using System;
using TechFood.Domain.Shared.Interfaces;

namespace TechFood.Domain.Events.Order;

public record class OrderCancelledEvent(
    Guid Id,
    DateTime CancelledAt) : IDomainEvent;
