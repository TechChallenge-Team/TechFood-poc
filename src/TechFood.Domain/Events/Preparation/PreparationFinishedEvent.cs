using System;
using TechFood.Domain.Shared.Interfaces;

namespace TechFood.Domain.Events.Preparation;

public record class PreparationFinishedEvent(
    Guid Id,
    Guid OrderId,
    DateTime FinishedAt) : IDomainEvent;
