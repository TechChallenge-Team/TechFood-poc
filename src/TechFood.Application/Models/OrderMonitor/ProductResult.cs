using System;

namespace TechFood.Application.Models.OrderMonitor;

public class ProductResult
{
    public Guid Id { get; set; }

    public string? ImageUrl { get; set; }

    public string Name { get; set; } = null!;

    public int Quantity { get; set; }

    public void SetImage(string image)
        => ImageUrl = image;
}
