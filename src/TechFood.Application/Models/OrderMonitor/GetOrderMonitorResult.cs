using System;
using System.Collections.Generic;
using TechFood.Domain.Enums;

namespace TechFood.Application.Models.OrderMonitor;

public class GetOrderMonitorResult
{
    public Guid OrderId { get; set; }

    public int Number { get; set; }

    public OrderStatusType Status { get; set; }

    public List<ProductResult> Products { get; set; }
}
