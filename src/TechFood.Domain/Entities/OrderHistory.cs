using System;
using TechFood.Domain.Enums;
using TechFood.Domain.Shared.Entities;

namespace TechFood.Domain.Entities;

public class OrderHistory : Entity
{
    public OrderHistory() { }

    public OrderHistory(
        Guid orderId,
        OrderStatusType orderStatusType
        )
    {
        OrderId = orderId;
        OrderStatusType = orderStatusType;
        CreatedAt = DateTime.Now;
    }

    public Guid OrderId { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public OrderStatusType OrderStatusType { get; private set; }
}
