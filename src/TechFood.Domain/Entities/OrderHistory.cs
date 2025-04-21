using System;
using TechFood.Domain.Enums;
using TechFood.Domain.Shared.Entities;

namespace TechFood.Domain.Entities;

public class OrderHistory : Entity
{
    public OrderHistory() { }

    public OrderHistory(
        Guid orderId,
        OrderStatusType status
        )
    {
        OrderId = orderId;
        Status = status;
        CreatedAt = DateTime.Now;
    }

    public Guid OrderId { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public OrderStatusType Status { get; private set; }
}
