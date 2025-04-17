using System;
using TechFood.Domain.Shared.Entities;

namespace TechFood.Domain.Entities;

public class Product : Entity
{
    public Product(
        string name,
        string description,
        Category category,
        Guid categoryId,
        string imageId,
        decimal price
        )
    {
        Name = name;
        Description = description;
        Category = category;
        CategoryId = categoryId;
        ImageId = imageId;
        Price = price;
    }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public Category Category { get; private set; }

    public Guid CategoryId { get; private set; }

    public string ImageId { get; private set; }

    public decimal Price { get; private set; }
}
