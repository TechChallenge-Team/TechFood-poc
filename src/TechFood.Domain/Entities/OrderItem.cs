using System;
using TechFood.Domain.Shared.Entities;

namespace TechFood.Domain.Entities;

public class OrderItem : Entity
{
    public OrderItem() { }

    public OrderItem(
        Order order,
        Product product,
        int quantity)
    {
        OrderId = order.Id;
        Order = order;
        ProductId = product.Id;
        Product = product;
        Quantity = quantity;
    }

    public Guid OrderId { get; private set; }

    public Order Order { get; private set; } = null!;

    public Guid ProductId { get; private set; }

    public Product Product { get; private set; } = null!;

    public int Quantity { get; private set; }
}
