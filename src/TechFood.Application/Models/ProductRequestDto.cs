using System;

namespace TechFood.Application.Models;

public class ProductRequestDto
{
    public ProductRequestDto(
        string name,
        string description,
        Guid categoryId,
        string imageFileName,
        decimal price
        )
    {
        Name = name;
        Description = description;
        CategoryId = categoryId;
        ImageFileName = imageFileName;
        Price = price;
    }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public Guid CategoryId { get; private set; }

    public string ImageFileName { get; private set; }

    public decimal Price { get; private set; }
}
