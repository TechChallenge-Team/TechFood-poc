using System;
using TechFood.Domain.Enums;
using TechFood.Domain.Shared.Entities;

namespace TechFood.Domain.Entities;

public class OrderHistory : Entity
{
    public OrderHistory() { }

    public OrderHistory(
        OrderStatusType status
        )
    {
        Status = status;
        CreatedAt = DateTime.Now;
    }

    public DateTime CreatedAt { get; private set; }

    public OrderStatusType Status { get; private set; }
}
