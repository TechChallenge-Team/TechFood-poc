using System;
using TechFood.Domain.Shared.Entities;

namespace TechFood.Domain.Entities;

public class OrderItem : Entity
{
    public OrderItem() { }

    public OrderItem(
        Guid productId,
        decimal unitPrice,
        int quantity)
    {
        ProductId = productId;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }

    public Guid ProductId { get; private set; }

    public decimal UnitPrice { get; private set; }

    public int Quantity { get; private set; }
}
