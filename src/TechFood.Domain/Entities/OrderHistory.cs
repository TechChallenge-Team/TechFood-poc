using System;
using TechFood.Domain.Enums;
using TechFood.Domain.Shared.Entities;

namespace TechFood.Domain.Entities;

public class OrderHistory : Entity
{
    public OrderHistory() { }

    public OrderHistory(
        Order order,
        OrderStatusType status
        )
    {
        OrderId = order.Id;
        Order = order;
        Status = status;
        CreatedAt = DateTime.Now;
    }

    public Order Order { get; private set; } = null!;

    public Guid OrderId { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public OrderStatusType Status { get; private set; }
}
