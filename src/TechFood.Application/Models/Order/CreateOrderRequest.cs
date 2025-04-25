using System;
using System.ComponentModel.DataAnnotations;

namespace TechFood.Application.Models.Order
{
    public class CreateOrderRequest
    {
        [Required]
        public Guid? CustomerId { get; set; }
    }
}
