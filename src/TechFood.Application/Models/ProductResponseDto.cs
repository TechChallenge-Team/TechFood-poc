using System;

namespace TechFood.Application.Models;

public class ProductResponseDto
{
    public ProductResponseDto(
        Guid id,
        string name,
        string description,
        Guid categoryId,
        bool outOfStock,
        string imageFileName,
        decimal price
        )
    {
        Id = id;
        Name = name;
        Description = description;
        CategoryId = categoryId;
        OutOfStock = outOfStock;
        ImageFileName = imageFileName;
        Price = price;
    }

    public Guid Id { get; set; }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public Guid CategoryId { get; private set; }

    public bool OutOfStock { get; private set; }

    public string ImageFileName { get; private set; }

    public decimal Price { get; private set; }
}
