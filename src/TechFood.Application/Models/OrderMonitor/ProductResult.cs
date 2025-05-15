using System;

namespace TechFood.Application.Models.OrderMonitor;

public class ProductResult
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
}
