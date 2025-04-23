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
        string imageFileName,
        decimal price
        )
    {
        SetName(name);
        SetDescription(description);
        SetCategory(categoryId);
        SetImageFileName(imageFileName);
        SetPrice(price);
        SetOutOfStock(false);
    }
    
    public string Name { get; private set; } = null!;

    public string Description { get; private set; } = null!;

    public Guid CategoryId { get; private set; }

    public bool OutOfStock { get; private set; }

    public string ImageFileName { get; private set; } = null!;

    public decimal Price { get; private set; }

    public void SetOutOfStock(bool outOfStock)
    {
        OutOfStock = outOfStock;
    }

    public void SetImageFileName(string imageFileName = null!)
    {
        ImageFileName = imageFileName;
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Nome inválido.");
        }

        Name = name;
    }

    public void SetDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("Description Inválida.");
        }

        Description = description;
    }

    public void SetPrice(decimal price)
    {
        if (price <= 0)
        {
            throw new ArgumentException("Preço do producto não deve ser inferior a zero.");
        }

        Price = price;
    }

    public void SetCategory(Guid categoryId)
        => CategoryId = categoryId;

    public void Update(string name, string description, decimal price, Guid categoryId)
    {
        SetName(name);
        SetDescription(description);
        SetPrice(price);
        SetCategory(categoryId);
    }
}
