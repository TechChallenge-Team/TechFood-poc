using System;
using System.Collections.Generic;
using TechFood.Domain.Enums;
using TechFood.Domain.Shared.Entities;
using TechFood.Domain.Shared.Exceptions;

namespace TechFood.Domain.Entities;

public class Order : Entity, IAggregateRoot
{
    private Order() { }

    public Order(
        Customer customer)
    {
        Customer = customer;
        CustomerId = customer.Id;
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

    public Guid PaymentId { get; private set; }

    public Payment? Payment { get; private set; }

    public IReadOnlyCollection<OrderItem> Items => _itens.AsReadOnly();

    public IReadOnlyCollection<OrderHistory> Historical => _historical.AsReadOnly();

    public void CreatePayment(PaymentType type)
    {
        if (Status != OrderStatusType.Created)
        {
            throw new DomainException(Resources.Exceptions.Order_CannotCreatePaymentToNonCreatedStatus);
        }

        Payment = new Payment(this, type, TotalAmount);
        PaymentId = Payment.Id;
    }

    public void ApplyDiscount(decimal discount)
    {
        if (Status != OrderStatusType.Created)
        {
            throw new DomainException(Resources.Exceptions.Order_CannotApplyDiscountToNonCreatedStatus);
        }

        if (discount < 0)
        {
            throw new DomainException(Resources.Exceptions.Order_DiscountCannotBeNegative);
        }

        Discount = discount;

        CalculateAmount();
    }

    public void PayPayment()
    {
        if (Status != OrderStatusType.Created)
        {
            throw new DomainException(Resources.Exceptions.Order_CannotPayToNonCreatedStatus);
        }

        if (Payment == null)
        {
            throw new DomainException(Resources.Exceptions.Order_PaymentIsNull);
        }

        Payment.Pay();
        Status = OrderStatusType.Paid;

        _historical.Add(new(this, OrderStatusType.Paid));
    }

    public void RefusedPayment()
    {
        if (Status != OrderStatusType.Created)
        {
            throw new DomainException(Resources.Exceptions.Order_CannotRefusePaymentToNonCreatedStatus);
        }

        if (Payment == null)
        {
            throw new DomainException(Resources.Exceptions.Order_PaymentIsNull);
        }

        Payment.Refused();
    }

    public void AddItems(IEnumerable<OrderItem> items)
    {
        foreach (var item in items)
        {
            AddItem(item);
        }
    }

    public void AddItem(OrderItem item)
    {
        if (Status != OrderStatusType.Created)
        {
            throw new DomainException(Resources.Exceptions.Order_CannotAddItemToNonCreatedStatus);
        }

        _itens.Add(item);

        CalculateAmount();
    }

    public void RemoveItem(OrderItem item)
    {
        if (Status != OrderStatusType.Created)
        {
            throw new DomainException(Resources.Exceptions.Order_CannotRemoveItemToNonCreatedStatus);
        }

        _itens.Remove(item);

        CalculateAmount();
    }

    public void Prepare()
    {
        if (Status != OrderStatusType.Paid)
        {
            throw new DomainException(Resources.Exceptions.Order_CannotPrepareToNonPaidStatus);
        }

        Status = OrderStatusType.InPreparation;

        _historical.Add(new(this, OrderStatusType.InPreparation));
    }

    private void CalculateAmount()
    {
        Amount = 0;

        foreach (var item in _itens)
        {
            Amount += item.Quantity * item.Product.Price;
        }
    }

    public void Done()
    {
        if (Status != OrderStatusType.InPreparation)
        {
            throw new DomainException(Resources.Exceptions.Order_CannotFinishToNonInPreparationStatus);
        }

        Status = OrderStatusType.Done;

        _historical.Add(new(this, OrderStatusType.Done));
    }

    public void Finish()
    {
        FinishedAt = DateTime.Now;
        Status = OrderStatusType.Finished;

        _historical.Add(new(this, OrderStatusType.Finished));
    }
}
