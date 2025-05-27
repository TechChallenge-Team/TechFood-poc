using System;

namespace TechFood.Application.Models.Customer;

public class UpdateProductResult
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public Guid CategoryId { get; set; }

    public bool OutOfStock { get; set; }

    public string ImageUrl { get; set; }

    public decimal Price { get; set; }
}
