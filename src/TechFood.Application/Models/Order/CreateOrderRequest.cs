using System;
using System.Collections.Generic;

namespace TechFood.Application.Models.Order;

public class CreateOrderRequest
{
    public Guid? CustomerId { get; set; }

    public string? CuponCode { get; set; }

    public List<Item> Items { get; set; } = [];

    public class Item
    {
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
