using System;
using TechFood.Domain.Shared.Entities;

namespace TechFood.Domain.Entities;

public class Product : Entity, IAggregateRoot
{
    public Product() { }

    public Product(
        string name,
        string description,
        Guid categoryId,
        string imageId,
        decimal price
        )
    {
        Name = name;
        Description = description;
        CategoryId = categoryId;
        ImageId = imageId;
        Price = price;
    }

    public string Name { get; private set; } = null!;

    public string Description { get; private set; } = null!;

    public Category Category { get; private set; } = null!;

    public Guid CategoryId { get; private set; }

    public string ImageId { get; private set; } = null!;

    public decimal Price { get; private set; }
}
