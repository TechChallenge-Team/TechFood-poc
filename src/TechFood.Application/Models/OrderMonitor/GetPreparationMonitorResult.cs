using System;
using System.Collections.Generic;
using TechFood.Domain.Enums;

namespace TechFood.Application.Models.OrderMonitor;

public class GetPreparationMonitorResult
{
    public Guid preparationId { get; set; }

    public Guid OrderId { get; set; }

    public int Number { get; set; }

    public PreparationStatusType Status { get; set; }

    public IEnumerable<ProductResult>? Products { get; set; }
}

public class ProductResult
{
    public string? ImageUrl { get; set; }

    public string Name { get; set; } = null!;

    public int Quantity { get; set; }

    public void SetImage(string image)
        => ImageUrl = image;
}
