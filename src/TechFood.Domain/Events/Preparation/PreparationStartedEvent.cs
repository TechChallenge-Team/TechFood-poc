using System;
using TechFood.Domain.Shared.Interfaces;

namespace TechFood.Domain.Events.Preparation;

public record class PreparationStartedEvent(
    Guid Id,
    Guid OrderId,
    DateTime StartedAt) : IDomainEvent;
