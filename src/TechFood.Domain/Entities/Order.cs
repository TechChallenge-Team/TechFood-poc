using System;
using System.Collections.Generic;
using TechFood.Domain.Enums;
using TechFood.Domain.Shared.Entities;
using TechFood.Domain.Shared.Exceptions;
using TechFood.Domain.Shared.Validations;

namespace TechFood.Domain.Entities;

public class Order : Entity, IAggregateRoot
{
    private Order() { }

    public Order(
        int number,
        Guid? customerId = null)
    {
        Number = number;
        CustomerId = customerId;
        CreatedAt = DateTime.Now;
        Status = OrderStatusType.Created;
    }

    private readonly List<OrderItem> _items = [];

    private readonly List<OrderHistory> _historical = [];

    public int Number { get; private set; }

    public Guid? CustomerId { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime? FinishedAt { get; private set; }

    public OrderStatusType Status { get; private set; }

    public decimal Amount { get; private set; }

    public decimal Discount { get; private set; }

    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    public IReadOnlyCollection<OrderHistory> Historical => _historical.AsReadOnly();

    public void ApplyDiscount(decimal discount)
    {
        if (Status != OrderStatusType.Created)
        {
            throw new DomainException(Resources.Exceptions.Order_CannotApplyDiscountToNonCreatedStatus);
        }

        Validations.ThrowIsGreaterThanZero(discount, Resources.Exceptions.Order_DiscountCannotBeNegative);

        Discount = discount;

        CalculateAmount();
    }

    public void CreatePayment()
    {
        if (Status != OrderStatusType.Created)
        {
            throw new DomainException(Resources.Exceptions.Order_CannotCreatePaymentToNonCreatedStatus);
        }

        UpdateStatus(OrderStatusType.WaitingPayment);
    }

    public void PayPayment()
    {
        if (Status != OrderStatusType.Created)
        {
            throw new DomainException(Resources.Exceptions.Order_CannotPayToNonCreatedStatus);
        }

        UpdateStatus(OrderStatusType.Received);
    }

    public void RefusedPayment()
    {
        if (Status != OrderStatusType.Created)
        {
            throw new DomainException(Resources.Exceptions.Order_CannotRefusePaymentToNonCreatedStatus);
        }

        UpdateStatus(OrderStatusType.RefusedPayment);
    }

    public void AddItem(OrderItem item)
    {
        if (Status != OrderStatusType.Created)
        {
            throw new DomainException(Resources.Exceptions.Order_CannotAddItemToNonCreatedStatus);
        }

        _items.Add(item);

        CalculateAmount();
    }

    public void RemoveItem(Guid itemId)
    {
        if (Status != OrderStatusType.Created)
        {
            throw new DomainException(Resources.Exceptions.Order_CannotRemoveItemToNonCreatedStatus);
        }

        var item = _items.Find(i => i.Id == itemId);

        Validations.ThrowObjectIsNull(item, Resources.Exceptions.Order_ItemNotFound);

        _items.Remove(item!);

        CalculateAmount();
    }

    private void CalculateAmount()
    {
        Amount = 0;

        foreach (var item in _items)
        {
            Amount += item.Quantity * item.UnitPrice;
        }

        Amount -= Discount;
    }

    public void Done()
    {
        if (Status != OrderStatusType.InPreparation)
        {
            throw new DomainException(Resources.Exceptions.Order_CannotFinishToNonInPreparationStatus);
        }

        UpdateStatus(OrderStatusType.Done);
    }

    public void Prepare()
    {
        if (Status != OrderStatusType.Received)
        {
            throw new DomainException(Resources.Exceptions.Order_CannotPrepareToNonPaidStatus);
        }

        UpdateStatus(OrderStatusType.InPreparation);
    }

    public void Finish()
    {
        FinishedAt = DateTime.Now;

        UpdateStatus(OrderStatusType.Finished);
    }

    private void UpdateStatus(OrderStatusType status)
    {
        Status = status;
        _historical.Add(new(status));
    }
}
