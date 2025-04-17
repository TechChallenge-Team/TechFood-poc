using System;
using TechFood.Domain.Enums;
using TechFood.Domain.Shared.Entities;

namespace TechFood.Domain.Entities;

public class OrderHistory : Entity
{
    public OrderHistory(
        Guid orderId,
        DateTime createdAt,
        OrderStatusType orderStatusType
        )
    {
        OrderId = orderId;
        CreatedAt = createdAt;
        OrderStatusType = orderStatusType;
    }

    public Guid OrderId { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public OrderStatusType OrderStatusType { get; private set; }
}
