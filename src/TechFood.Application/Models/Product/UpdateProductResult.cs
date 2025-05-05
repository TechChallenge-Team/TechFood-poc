using System;

namespace TechFood.Application.Models.Customer;

public class UpdateProductResult
{
    public Guid Id { get; set; }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public Guid CategoryId { get; private set; }

    public bool OutOfStock { get; private set; }

    public string ImageFileName { get; private set; }

    public decimal Price { get; private set; }
}
