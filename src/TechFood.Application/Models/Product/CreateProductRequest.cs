using System;
using System.ComponentModel.DataAnnotations;

namespace TechFood.Application.Models.Product;

public class CreateProductRequest
{
    public CreateProductRequest(
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

    [Required]
    public string Name { get; private set; }

    [Required]
    public string Description { get; private set; }

    [Required]
    public Guid CategoryId { get; private set; }

    [Required]
    public string ImageFileName { get; private set; }

    [Required]
    public decimal Price { get; private set; }
}
