using System;
using TechFood.Domain.Shared.Interfaces;

namespace TechFood.Domain.Events.Preparation;

public record class PreparationCreatedEvent(
    Guid Id,
    Guid OrderId,
    DateTime CreatedAt) : IDomainEvent;
