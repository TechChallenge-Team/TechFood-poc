using System.ComponentModel.DataAnnotations;

namespace TechFoodClean.Domain.Entities;

public class CreateOrderRequestDTO
{
    [Required]
    public Guid? CustomerId { get; private set; }

    [Required]
    public string? CuponCode { get; private set; }

    [Required]
    public List<Item> Items { get; private set; } = [];

    public class Item
    {
        [Required]
        public Guid ProductId { get; private set; }

        [Required]
        public int Quantity { get; private set; }
    }
}
