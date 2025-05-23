using System;
using TechFood.Domain.Enums;
using TechFood.Domain.Events.Preparation;
using TechFood.Domain.Shared.Entities;
using TechFood.Domain.Shared.Exceptions;

namespace TechFood.Domain.Entities;

public class Preparation : Entity, IAggregateRoot
{
    private Preparation() { }

    public Preparation(Guid orderId)
    {
        OrderId = orderId;
        CreatedAt = DateTime.Now;
        Status = PreparationStatusType.Pending;

        _events.Add(new PreparationCreatedEvent(
            Id,
            OrderId,
            CreatedAt));
    }

    public Guid OrderId { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime? StartedAt { get; private set; }

    public DateTime? FinishedAt { get; private set; }

    public PreparationStatusType Status { get; private set; }

    public void Start()
    {
        if (Status != PreparationStatusType.Pending)
        {
            throw new DomainException(Resources.Exceptions.Preparation_CanOnlyStartIfInPending);
        }

        StartedAt = DateTime.Now;
        Status = PreparationStatusType.InProgress;

        _events.Add(new PreparationStartedEvent(
            Id,
            OrderId,
            StartedAt.Value));
    }

    public void Finish()
    {
        if (Status != PreparationStatusType.InProgress)
        {
            throw new DomainException(Resources.Exceptions.Preparation_CanOnlyFinishIfInProgress);
        }

        Status = PreparationStatusType.Done;
        FinishedAt = DateTime.Now;

        _events.Add(new PreparationFinishedEvent(
            Id,
            OrderId,
            FinishedAt.Value));
    }
}
