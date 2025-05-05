using System;

namespace TechFood.Application.Models.Order;

public class CreateOrderRequest
{
    public Guid? CustomerId { get; set; }
}
