using System;
using TechFood.Domain.Shared.Entities;
using TechFood.Domain.Shared.Validations;

namespace TechFood.Domain.Entities;

public class OrderItem : Entity
{
    private OrderItem() { }

    public OrderItem(
        Guid productId,
        decimal unitPrice,
        int quantity)
    {
        ProductId = productId;
        UnitPrice = unitPrice;
        Quantity = quantity;

        Validate();
    }

    public Guid ProductId { get; private set; }

    public decimal UnitPrice { get; private set; }

    public int Quantity { get; private set; }

    private void Validate()
    {
        Validations.ThrowValidGuid(ProductId, Resources.Exceptions.OrderItem_ThrowProductIdIsInvalid);
        Validations.ThrowIsGreaterThanZero(UnitPrice, Resources.Exceptions.OrderItem_ThrowUnitPriceGreaterThanZero);
        Validations.ThrowIsGreaterThanZero(Quantity, Resources.Exceptions.OrderItem_ThrowQuantityGreaterThanZero);
    }
}
