using System;
using System.Collections.Generic;
using TechFood.Domain.Enums;
using TechFood.Domain.Shared.Entities;
using TechFood.Domain.Shared.Exceptions;
using TechFood.Domain.ValueObjects;

namespace TechFood.Domain.Entities;

public class Order : Entity, IAggregateRoot
{
    private Order() { }

    public Order(
        Guid customerId,
        decimal amount,
        decimal discount
        )
    {
        CustomerId = customerId;
        Amount = amount;
        Discount = discount;
        CreatedAt = DateTime.Now;
        Status = OrderStatusType.Created;
    }

    private readonly List<OrderItem> _itens = [];

    private readonly List<OrderHistory> _historical = [];

    public Guid CustomerId { get; private set; }

    public Customer Customer { get; private set; } = null!;

    public DateTime CreatedAt { get; private set; }

    public DateTime? FinishedAt { get; private set; }

    public OrderStatusType Status { get; private set; }

    public decimal Amount { get; private set; }

    public decimal TotalAmount => Amount - Discount;

    public decimal Discount { get; private set; }

    public Payment? Payment { get; private set; }

    public IReadOnlyCollection<OrderItem> Items => _itens.AsReadOnly();

    public IReadOnlyCollection<OrderHistory> Historical => _historical.AsReadOnly();

    public void Pay(Payment payment)
    {
        if (payment.Amount != TotalAmount)
        {
            throw new DomainException(Resources.Exceptions.Order_AmountIsNotEqualOrderAmount);
        }

        Payment = payment;
        Status = OrderStatusType.Paid;

        _historical.Add(new(Id, OrderStatusType.Paid));
    }

    public void AddItem(OrderItem item)
    {
        if (Status != OrderStatusType.Created)
        {
            throw new DomainException(Resources.Exceptions.Order_CannotAddItemToNonCreatedStatus);
        }

        _itens.Add(item);
    }

    public void RemoveItem(OrderItem item)
    {
        if (Status != OrderStatusType.Created)
        {
            throw new DomainException(Resources.Exceptions.Order_CannotRemoveItemToNonCreatedStatus);
        }

        _itens.Remove(item);
    }

    public void Prepare()
    {
        if (Status != OrderStatusType.Paid)
        {
            throw new DomainException(Resources.Exceptions.Order_CannotPrepareToNonPaidStatus);
        }

        Status = OrderStatusType.InPreparation;

        _historical.Add(new(Id, OrderStatusType.InPreparation));
    }

    public void Done()
    {
        if (Status != OrderStatusType.InPreparation)
        {
            throw new DomainException(Resources.Exceptions.Order_CannotFinishToNonInPreparationStatus);
        }

        Status = OrderStatusType.Done;

        _historical.Add(new(Id, OrderStatusType.Done));
    }

    public void Finish()
    {
        FinishedAt = DateTime.Now;
        Status = OrderStatusType.Finished;

        _historical.Add(new(Id, OrderStatusType.Finished));
    }
}
