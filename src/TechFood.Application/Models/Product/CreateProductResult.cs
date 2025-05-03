using System;

namespace TechFood.Application.Models.Product;

public class CreateProductResult
{
    public CreateProductResult(Guid id)
        => Id = id;

    public Guid Id { get; set; }
}
