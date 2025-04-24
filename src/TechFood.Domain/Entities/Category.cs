using TechFood.Domain.Shared.Entities;
using TechFood.Domain.Shared.Exceptions;
using TechFood.Domain.Shared.Validations;

namespace TechFood.Domain.Entities;

public class Category : Entity, IAggregateRoot
{
    public Category() { }

    public Category(string name, string imageFileName)
    {
        Name = name;
        ImageFileName = imageFileName;

        Validate();
    }

    public string Name { get; private set; } = null!;

    public string ImageFileName { get; private set; } = null!;

    public void UpdateCategory(string name, string imageFileName)
    {
        Name = name;
        ImageFileName = imageFileName;
    }

    private void Validate()
    {
        Validations.ThrowIfEmpty(Name, "The category name cannot be empty");
        Validations.ThrowIfEmpty(ImageFileName, "The category file image cannot be empty");
    }
}
