using System;
using System.Collections.Generic;
using TechFood.Domain.Enums;
using TechFood.Domain.Shared.Entities;
using TechFood.Domain.ValueObjects;

namespace TechFood.Domain.Entities;

public class Order : Entity, IAggregateRoot
{
    private Order() { }

    public Order(
        Guid customerId,
        Customer customer,
        OrderStatusType orderStatusType,
        decimal price,
        decimal discount,
        List<OrderItem> items
        )
    {
        CustomerId = customerId;
        Customer = customer;
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
        OrderStatusType = orderStatusType;
        Price = price;
        Discount = discount;
        _itens = items;
    }

    private readonly List<OrderItem> _itens = [];

    private readonly List<OrderHistory> _history = [];

    public Guid CustomerId { get; private set; }

    public Customer Customer { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    public DateTime? FinishedAt { get; private set; }

    public OrderStatusType OrderStatusType { get; private set; }

    public decimal Price { get; private set; }

    public decimal Discount { get; private set; }

    public Payment? Payment { get; private set; }

    public IReadOnlyCollection<OrderItem> Items => _itens.AsReadOnly();

    public IReadOnlyCollection<OrderHistory> Historical => _history.AsReadOnly();
}
