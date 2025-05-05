using System;
using System.ComponentModel.DataAnnotations;

namespace TechFood.Application.Models.Order;

public class AddOrderItemRequest
{
    [Required]
    public Guid ProductId { get; set; }

    [Range(0, int.MaxValue)]
    public int Quantity { get; set; }
}
