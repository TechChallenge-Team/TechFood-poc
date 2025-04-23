using TechFood.Domain.Shared.Entities;

namespace TechFood.Domain.Entities;

public class Category : Entity, IAggregateRoot
{
    private Category() { }

    public Category(string name, string imageFileName)
    {
        Name = name;
        ImageFileName = imageFileName;
    }

    public string Name { get; private set; } = null!;

    public string ImageFileName { get; private set; } = null!;
}
