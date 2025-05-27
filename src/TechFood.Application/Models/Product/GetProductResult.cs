using System;

namespace TechFood.Application.Models.Product;

public class GetProductResult
{

    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public Guid CategoryId { get; set; }

    public bool OutOfStock { get; set; }

    public string ImageUrl { get; set; }

    public decimal Price { get; set; }
}
