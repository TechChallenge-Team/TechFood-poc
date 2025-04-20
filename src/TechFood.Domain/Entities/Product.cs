using System;
using TechFood.Domain.Shared.Entities;

namespace TechFood.Domain.Entities;

public class Product : Entity, IAggregateRoot
{
    public Product() { }

    public Product(
        string name,
        string description,
        Category category,
        string imageFileName,
        decimal price
        )
    {
        Name = name;
        Description = description;
        Category = category;
        CategoryId = category.Id;
        ImageFileName = imageFileName;
        Price = price;
    }

    public string Name { get; private set; } = null!;

    public string Description { get; private set; } = null!;

    public Category Category { get; private set; } = null!;

    public Guid CategoryId { get; private set; }

    public bool OutOfStock { get; private set; }

    public string ImageFileName { get; private set; } = null!;

    public decimal Price { get; private set; }

    public void SetOutOfStock() => OutOfStock = true;

    public void SetInStock() => OutOfStock = false;
}
