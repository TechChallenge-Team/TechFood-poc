using System.ComponentModel.DataAnnotations;

namespace TechFoodClean.Domain.Entities;

public class CreateOrderRequestDTO
{
    [Required]
    public Guid CustomerId { get; set; }

    public string? CuponCode { get; set; }

    [Required]
    public List<Item> Items { get; set; } = [];

    public class Item
    {
        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
