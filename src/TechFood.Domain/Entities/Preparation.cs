using System;
using TechFood.Domain.Enums;
using TechFood.Domain.Shared.Entities;

namespace TechFood.Domain.Entities;

public class Preparation : Entity, IAggregateRoot
{
    private Preparation() { }

    public Preparation(Guid orderId)
    {
        OrderId = orderId;
        CreatedAt = DateTime.Now;
        Status = PreparationStatusType.Pending;
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
            throw new InvalidOperationException("Preparation can only be started if it is pending.");
        }

        StartedAt = DateTime.Now;
        Status = PreparationStatusType.InProgress;
    }

    public void Finish()
    {
        if (Status != PreparationStatusType.InProgress)
        {
            throw new InvalidOperationException("Preparation can only be finished if it is in progress.");
        }

        Status = PreparationStatusType.Done;
        FinishedAt = DateTime.Now;
    }
}
